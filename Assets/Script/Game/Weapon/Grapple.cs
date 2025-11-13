using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class Grapple : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask grappleLayer;
    public LineRenderer lineRenderer;
    public float pullForce = 100f;  // 목표점으로 당기는 힘
    public float maxSpeed = 150f;   // 최대 스피드
    public float maxDistance = 100f;     // 최대 그래플 거리
    public bool IsGrappling => isGrappling;

    private float ropeLength;   // 로프 길이
    private bool isGrappling = false;   // 그래플 여부
    private Vector3 grapplePoint;   // 충돌 지점
    private Rigidbody rb;
    private RaycastDebugger raycastDebugger;
    private PlayerDash playerDash;
    private Cursor cursor;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerDash = FindObjectOfType<PlayerDash>();
        raycastDebugger = FindObjectOfType<RaycastDebugger>();
        cursor = FindObjectOfType<Cursor>();

        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartGrapple();

        if (Input.GetKeyUp(KeyCode.F) || (playerDash != null && playerDash.IsDashing))
            StopGrapple();

        if (isGrappling && lineRenderer != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    void FixedUpdate()
    {
        if (!isGrappling) return;

        Vector3 toGrapple = grapplePoint - transform.position;  // 거리, 방향
        Vector3 dir = toGrapple.normalized; // 방향
        float distance = toGrapple.magnitude;   // 거리

        // 목표점으로 가속
        float dynamicForce = Mathf.Lerp(0, pullForce, distance / maxDistance);  // 보간
        rb.AddForce(dir * dynamicForce, ForceMode.Acceleration);    // 끌어 당기기

        // 좌우 스윙 제어 (A/D 키)
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 swingDir = Vector3.Cross(dir, Vector3.down).normalized * horizontal;
        rb.AddForce(swingDir * pullForce * 0.3f, ForceMode.Acceleration);

        // 너무 빠르면 감속
        float maxSpeed = 50f;
        if (rb.velocity.magnitude > maxSpeed) // 현재 속력 > 최대 속력
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;    // 속력은 = 방향1 * 최대 속력
        }
        // 로프 길이 조절, ============================================================= 조정 할 거임 ============================================================= 
        if (distance > ropeLength)
        {
            rb.velocity = Vector3.ProjectOnPlane(rb.velocity, dir); // 줄 방향 속도 제거
            rb.AddForce(dir * pullForce, ForceMode.Acceleration);
        }
    }

    void StartGrapple()
    {
        Ray ray = raycastDebugger.GetViewRay();

        float sphereRaius = (cursor!= null) ? cursor.sphereRadius : 2f;
        if (Physics.SphereCast(ray, sphereRaius, out RaycastHit hit, maxDistance, grappleLayer))    // 점이 아니라 , 원으로 
        {
            grapplePoint = hit.point;
            isGrappling = true;
            ropeLength = Vector3.Distance(transform.position, grapplePoint); // 줄 길이 고정

            if (lineRenderer != null)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.enabled = true;
            }

            Debug.Log($"🪝 Grapple 시작: {hit.collider.name} @ {hit.point}");
        }
        else
        {
            Debug.Log("❌ Grapple 대상 없음!");
        }
    }

    void StopGrapple()
    {
        isGrappling = false;
        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }
}
