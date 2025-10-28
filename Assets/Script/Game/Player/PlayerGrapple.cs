using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RealisticSwingGrapple : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LayerMask grappleLayer;
    public float maxGrappleDistance = 30f;
    public float springStrength = 50f;  // 스프링처럼 당기는 힘
    public float damper = 5f;           // 흔들림 감쇠 (높을수록 진동 줄어듦)
    public float stopDistance = 2f;     // 너무 가까우면 해제

    private Rigidbody rb;
    private ConfigurableJoint joint;
    private Vector3 grapplePoint;
    private bool isGrappling = false;
    private float grappleLength;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartGrapple();

        if (Input.GetMouseButtonUp(0))
            StopGrapple();

        if (isGrappling)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    void StartGrapple()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxGrappleDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            grappleLength = Vector3.Distance(transform.position, grapplePoint);
            isGrappling = true;

            // 줄 시각화
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);

            // 조인트 생성
            joint = gameObject.AddComponent<ConfigurableJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            // 줄 길이 고정
            SoftJointLimit limit = new SoftJointLimit();
            limit.limit = grappleLength;
            joint.linearLimit = limit;

            // 스프링처럼 당기기 설정
            JointDrive drive = new JointDrive();
            drive.positionSpring = springStrength;
            drive.positionDamper = damper;
            drive.maximumForce = Mathf.Infinity;
            joint.xDrive = joint.yDrive = joint.zDrive = drive;

            joint.xMotion = ConfigurableJointMotion.Limited;
            joint.yMotion = ConfigurableJointMotion.Limited;
            joint.zMotion = ConfigurableJointMotion.Limited;

            joint.enablePreprocessing = false;

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

    Vector3 toGrapple = grapplePoint - transform.position;
    float distance = toGrapple.magnitude;
    Vector3 dir = toGrapple.normalized;

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
