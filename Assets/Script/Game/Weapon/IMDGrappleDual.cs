using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class IMDGrappleDual : MonoBehaviour
{
    [Header("Settings")]
    public Camera playerCamera;
    public LayerMask grappleLayer;
    public float maxGrappleDistance = 30f;
    public float pullSpeed = 5f;
    public float swingForce = 15f;

    [Header("Line Renderers")]
    public LineRenderer lineLeft;
    public LineRenderer lineRight;

    private Rigidbody rb;

    // 좌/우 grapple 정보
    private bool isGrapplingLeft = false;
    private bool isGrapplingRight = false;
    private Vector3 hitLeft;
    private Vector3 hitRight;
    private float distLeft;
    private float distRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (playerCamera == null)
            playerCamera = Camera.main;

        if (lineLeft != null) { lineLeft.positionCount = 2; lineLeft.enabled = false; }
        if (lineRight != null) { lineRight.positionCount = 2; lineRight.enabled = false; }
    }

    void Update()
    {
        // 좌/우 grapple 발사
        if (Input.GetKeyDown(KeyCode.F)) TryGrapple(ref isGrapplingLeft, ref hitLeft, ref distLeft, lineLeft);
        if (Input.GetKeyDown(KeyCode.G)) TryGrapple(ref isGrapplingRight, ref hitRight, ref distRight, lineRight);

        // grapple 해제
        if (Input.GetKeyUp(KeyCode.F)) StopGrapple(ref isGrapplingLeft, lineLeft);
        if (Input.GetKeyUp(KeyCode.G)) StopGrapple(ref isGrapplingRight, lineRight);

        // 줄 길이 조절
        if (isGrapplingLeft) AdjustRopeLength(ref distLeft, KeyCode.Space);
        if (isGrapplingRight) AdjustRopeLength(ref distRight, KeyCode.Space);
    }

    void FixedUpdate()
    {
        // 두 줄이 동시에 있을 때 합성력 적용
        Vector3 force = Vector3.zero;

        if (isGrapplingLeft)
            force += (hitLeft - transform.position).normalized * swingForce;
        if (isGrapplingRight)
            force += (hitRight - transform.position).normalized * swingForce;

        // 이동 입력도 합성
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = playerCamera.transform.forward * v + playerCamera.transform.right * h;
        force += moveDir * swingForce;

        rb.AddForce(force, ForceMode.Acceleration);

        // LineRenderer 갱신
        if (isGrapplingLeft && lineLeft != null) { lineLeft.SetPosition(0, transform.position); lineLeft.SetPosition(1, hitLeft); }
        if (isGrapplingRight && lineRight != null) { lineRight.SetPosition(0, transform.position); lineRight.SetPosition(1, hitRight); }
    }

    void TryGrapple(ref bool isGrappling, ref Vector3 hitPoint, ref float distance, LineRenderer lr)
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, maxGrappleDistance, grappleLayer))
        {
            hitPoint = hit.point;
            distance = Vector3.Distance(transform.position, hitPoint);
            isGrappling = true;
            if (lr != null) lr.enabled = true;
        }
    }

    void StopGrapple(ref bool isGrappling, LineRenderer lr)
    {
        isGrappling = false;
        if (lr != null) lr.enabled = false;
    }

    void AdjustRopeLength(ref float distance, KeyCode key)
    {
        if (Input.GetKey(key))
        {
            distance -= pullSpeed * Time.fixedDeltaTime;
            distance = Mathf.Max(1f, distance);
        }
    }
}
