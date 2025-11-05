using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerGrapple : MonoBehaviour
{
    [Header("Grapple Settings")]
    public LineRenderer lineRenderer;   // 그래플 줄을 시각화하기 위한 LineRenderer
    public LayerMask grappleLayer;      // 줄이 닿을 수 있는 대상 레이어
    public float maxGrappleDistance = 30f; // 최대 줄 길이
    public float springStrength = 50f;     // 줄이 당기는 힘
    public float damper = 5f;              // 줄의 감쇠력

    public bool IsGrappling { get; private set; } // 그래플 상태 확인
    public PlayerRolling playerRolling;   // 그래플 상태 참조
    public PlayerCursor playerCursor;     // 커서 정보 참조

    private Rigidbody rb;              // 플레이어 Rigidbody
    private ConfigurableJoint joint;   // 줄 연결을 위한 Joint

    void Awake()
    {
        // 플레이어 Rigidbody 가져오기
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // F키 누르면 그래플 시작
        if (Input.GetKeyDown(KeyCode.F)) StartGrapple();
        // F키 떼면 그래플 종료
        if (Input.GetKeyUp(KeyCode.F)) StopGrapple();

        // 그래플 중이면 줄 시각화
        if (IsGrappling && lineRenderer != null)
        {
            lineRenderer.SetPosition(0, transform.position); // 플레이어 위치
            lineRenderer.SetPosition(1, playerCursor.AimPoint); // 목표 지점 위치 (hit.point 대신 AimPoint 사용)
        }
    }

    void FixedUpdate()
    {
        if (!IsGrappling) return; // 그래플링 상태가 아니면
    }

    // 🔹 그래플 시작 (커서 위치 기반으로)
    void StartGrapple()
    {
        // 커서에서 위치를 가져오기 (PlayerCursor에서 AimPoint를 가져옴)
        Vector3 grappleTarget = playerCursor.AimPoint;

        // 최대 그래플 길이 내에서만 그래플을 시작하도록 조건 추가
        if (Vector3.Distance(transform.position, grappleTarget) > maxGrappleDistance)
        {
            grappleTarget = transform.position + (grappleTarget - transform.position).normalized * maxGrappleDistance;
        }

        // 줄 시각화 세팅
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grappleTarget); // 목표 지점으로 그래플
        }

        // ConfigurableJoint 생성하여 플레이어와 목표점을 연결
        joint = gameObject.AddComponent<ConfigurableJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grappleTarget;

        // 줄 길이 제한
        SoftJointLimit limit = new SoftJointLimit();
        limit.limit = Vector3.Distance(transform.position, grappleTarget);
        joint.linearLimit = limit;

        // 줄의 스프링/댐퍼 세팅
        JointDrive drive = new JointDrive();
        drive.positionSpring = springStrength;
        drive.positionDamper = damper;
        drive.maximumForce = Mathf.Infinity;
        joint.xDrive = joint.yDrive = joint.zDrive = drive;

        // X/Y/Z 이동 제한 (줄 길이 이상 이동 금지)
        joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Limited;
        joint.enablePreprocessing = false;

        Debug.Log("그래플 시작!");
    }

    // 🔹 그래플 종료
    void StopGrapple()
    {
        // Joint 제거
        if (joint) Destroy(joint);
        IsGrappling = false;

        // 줄 시각화 끄기
        if (lineRenderer != null)
            lineRenderer.enabled = false;

        // 롤링 중이 아니면 카메라 방향 따라가기
        if (!playerRolling.IsRolling && Camera.main != null)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f);
            transform.rotation = targetRotation;
        }
    }
}
