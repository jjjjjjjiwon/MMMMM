using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RealisticSwingGrapple : MonoBehaviour
{
    public LineRenderer lineRenderer;   // 선 만들기
    public LayerMask grappleLayer;      // 레이어
    public float maxGrappleDistance = 30f; // 
    public float springStrength = 50f;  // 스프링처럼 당기는 힘
    public float damper = 5f;           // 흔들림 감쇠 (높을수록 진동 줄어듦)
    public float stopDistance = 2f;     // 너무 가까우면 해제

    private Rigidbody rb;
    private ConfigurableJoint joint;
    private Vector3 grapplePoint;       
    private bool isGrappling = false;   // 그래플링 가능 여부
    private float grappleLength;        // 그래플링 길이

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 초기화
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 좌클릭 누를 시
            StartGrapple();

        if (Input.GetMouseButtonUp(0))  // 좌클릭를 때면
            StopGrapple();

        if (isGrappling)    // true면 그래플링을 그린다
        {
            lineRenderer.SetPosition(0, transform.position);    // player 위치
            lineRenderer.SetPosition(1, grapplePoint);          // 충돌 위치
        }
    }

    void StartGrapple()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    // 

        if (Physics.Raycast(ray, out RaycastHit hit, maxGrappleDistance, grappleLayer)) // ray에서 발사해 최대거리, 레이어가 맞는지 확인하고 그 충돌값을 hit에 넣고 bool 값 반환
        {
            grapplePoint = hit.point;
            grappleLength = Vector3.Distance(transform.position, grapplePoint); // 길이 구하는
            isGrappling = true;

            // 줄 시각화
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2; // 줄을 만드는 점의 개수
            lineRenderer.SetPosition(0, transform.position);    // 줄의 처음 점
            lineRenderer.SetPosition(1, grapplePoint);          // 줄의 두번 째 점

            // 조인트 생성
            joint = gameObject.AddComponent<ConfigurableJoint>(); // joint 초기화하고 <ConfigurableJoint>객체 반환
            joint.autoConfigureConnectedAnchor = false; // joint(관절)을 자신이 연결하겠다
            joint.connectedAnchor = grapplePoint;   // joint(관절)을 grapplePoint에 연결하겠다

            // 줄 길이 고정
            SoftJointLimit limit = new SoftJointLimit();    // 관절의 이동이나 회전을 제한하기 위해
            limit.limit = grappleLength;    // grappleLength의 크기로 제한
            joint.linearLimit = limit;      // 이동 거리 제한을 limit까지로

            // 스프링처럼 당기기 설정
            JointDrive drive = new JointDrive();    // 관절을 움직이기 위해, 초기화
            drive.positionSpring = springStrength;  // 관절을 목표 위치로 밀어주는 힘, springStrength의 크기로 밀어주는
            drive.positionDamper = damper;  // damper만큼 흔들림 줄이기
            drive.maximumForce = Mathf.Infinity;    // 힘을 무한으로
            joint.xDrive = joint.yDrive = joint.zDrive = drive; // joint의 x,y,z축에 drive를 넣는다

            joint.xMotion = ConfigurableJointMotion.Limited; // x축 이동제한
            joint.yMotion = ConfigurableJointMotion.Limited; // y축 이동제한
            joint.zMotion = ConfigurableJointMotion.Limited; // z축 이동제한

            joint.enablePreprocessing = false;  // joint의 움직임을 unity가 안정화 하지 말아라

            Debug.Log($"🎯 그래플링 성공! 거리 {grappleLength:F2}");
        }
    }

    void StopGrapple()
    {
        if (joint) Destroy(joint);
        isGrappling = false;
        lineRenderer.enabled = false;
    }

    void FixedUpdate()
{
    if (!isGrappling) return;

    // 줄 시각적 업데이트
    lineRenderer.SetPosition(0, transform.position);
    lineRenderer.SetPosition(1, grapplePoint);

    Vector3 toGrapple = grapplePoint - transform.position; // transform.position에서 grapplePoint 가는 방향 , 거리을 넣는다
    float distance = toGrapple.magnitude;   // toGrapple의 거리를 넣는다
    Vector3 dir = toGrapple.normalized;     // toGrappl의 방향을 넣는다

    // 너무 가까워지면 자동 해제
    if (distance < stopDistance)
    {
        StopGrapple();
        return;
    }

    // 🎯 "액션 보정": 줄 방향으로 추가 가속 (끌림 + 손맛)
    rb.AddForce(dir * 15f, ForceMode.Acceleration);

    // 🎯 "스윙 보정": 플레이어의 측면 이동에 따라 수평 가속 강화
    Vector3 tangent = Vector3.Cross(Vector3.up, dir); 
    float input = Input.GetAxis("Horizontal");
    rb.AddForce(tangent * input * 20f, ForceMode.Acceleration);
}

}
