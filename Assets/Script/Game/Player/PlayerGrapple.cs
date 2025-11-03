using System.Collections;
using System.Collections.Generic;
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
    public float stopDistance = 2f;        // 목표점까지 가까워지면 자동 해제 거리

    public bool IsGrappling { get; private set; } // 그래플 상태 확인
    public Vector3 GrapplePoint => grapplePoint; // 외부 스크립트에서 줄 지점 접근 가능

    private Rigidbody rb;              // 플레이어 Rigidbody
    private ConfigurableJoint joint;   // 줄 연결을 위한 Joint
    private Vector3 grapplePoint;      // 줄이 닿은 지점


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
            lineRenderer.SetPosition(1, grapplePoint);       // 목표점 위치
        }
    }

    void FixedUpdate()
    {
        if (!IsGrappling) return; // 그래플링 상태가 아니면
    }

    void StartGrapple()
    {
        // 마우스 위치에서 레이 쏘기
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxGrappleDistance, grappleLayer))
        {
            grapplePoint = hit.point; // 맞은 지점을 grapplePoint로 저장
            float grappleLength = Vector3.Distance(transform.position, grapplePoint);
            IsGrappling = true;       // 그래플 시작

            // 줄 시각화 세팅
            if (lineRenderer != null)
            {
                lineRenderer.enabled = true;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, grapplePoint);
            }

            // ConfigurableJoint 생성하여 플레이어와 목표점을 연결
            joint = gameObject.AddComponent<ConfigurableJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            // 줄 길이 제한
            SoftJointLimit limit = new SoftJointLimit();
            limit.limit = grappleLength;
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

            Debug.Log(IsGrappling);
        }
    }

    void StopGrapple()
    {
        // Joint 제거
        if (joint) Destroy(joint);
        IsGrappling = false;

        // 줄 시각화 끄기
        if (lineRenderer != null) lineRenderer.enabled = false;
    }

}
