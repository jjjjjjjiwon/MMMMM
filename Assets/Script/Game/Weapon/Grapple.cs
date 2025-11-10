using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class Grapple : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask grappleLayer;        // 그래플이 닿을 수 있는 레이어
    public LineRenderer lineRenderer;     // 줄 시각화
    public float grappleSpeed = 20f;      // 플레이어 이동 속도
    public float maxDistance = 100f;       // 최대 그래플 거리

    private bool isGrappling = false;   // 그래플 여부
    private Vector3 grapplePoint;
    private Rigidbody rb;
    private PlayerDash playerDash;
    private RaycastDebugger raycastDebugger;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerDash = FindObjectOfType<PlayerDash>();

        if (raycastDebugger == null)
            raycastDebugger = FindObjectOfType<RaycastDebugger>();
        
        if (lineRenderer != null)
            lineRenderer.enabled = false;
            
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartGrapple();
        }

        if (Input.GetKeyUp(KeyCode.F) || playerDash.IsDashing)
        {
            StopGrapple();
        }

        // 줄 시각화
        if (isGrappling && lineRenderer != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    void FixedUpdate()
    {
         if (!isGrappling) return;

        Vector3 dir = (grapplePoint - transform.position).normalized;
        rb.MovePosition(rb.position + dir * grappleSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, grapplePoint) < 1f)
            StopGrapple();
    }

    void StartGrapple()
    {
         Ray ray = raycastDebugger.GetViewRay();

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            isGrappling = true;

            if (lineRenderer != null)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, grapplePoint);
            }

            Debug.Log($"🪝 그래플 시작: {hit.collider.name} @ {hit.point}");
        }
        else
        {
            Debug.Log("❌ 그래플할 대상 없음!");
        }
    }

    void StopGrapple()
    {
         isGrappling = false;
        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }
}
