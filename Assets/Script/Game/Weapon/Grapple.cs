using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class TitanGrappleController : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask grappleLayer;
    public LineRenderer lineRenderer;
    public float pullForce = 25f;       // 목표점으로 당기는 힘
    public float maxDistance = 100f;     // 최대 그래플 거리

    private float ropeLength;
    private bool isGrappling = false;
    private Vector3 grapplePoint;   // 충돌 지점
    private Rigidbody rb;
    private RaycastDebugger raycastDebugger;
    private PlayerDash playerDash;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerDash = FindObjectOfType<PlayerDash>();
        raycastDebugger = FindObjectOfType<RaycastDebugger>();

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

    Vector3 toGrapple = grapplePoint - transform.position; // 방향과 거리 둘다 가지고 있다
    float distance = toGrapple.magnitude; // 거리만 가져오기
    Vector3 dir = toGrapple.normalized; // 방향만 가져오기

    rb.AddForce(dir * pullForce, ForceMode.Force);   // 목표점으로 끌어당기는 역할

    float horizontal = Input.GetAxis("Horizontal"); // A/D 또는 ←/→
    // 앞뒤 입력(vertical)은 무시
    Vector3 swingDir = Vector3.Cross(dir, Vector3.up).normalized * horizontal; // 스윙 운동을 위해, 좀더 공부 필요
    rb.AddForce(swingDir, ForceMode.Force); // 스윙 힘 주기

        // 로프 길이 조절, ============================================================= 조정 할 거임 ============================================================= 
        if (distance > ropeLength)
        {
            Vector3 correction = dir * (distance - ropeLength);
            rb.MovePosition(transform.position + correction);
        }
}

    void StartGrapple()
    {
        Ray ray = raycastDebugger.GetViewRay();
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, grappleLayer))
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
