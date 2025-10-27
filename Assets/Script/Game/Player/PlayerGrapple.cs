using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerGrapple : MonoBehaviour
{
    public LineRenderer lineRenderer;       // 로프 시각화
    public LayerMask grappleLayer;          // 그래플 가능한 표면
    public float maxGrappleDistance = 30f;  // 사정거리
    public float pullSpeed = 15f;           // 끌려가는 속도

    private Rigidbody rb;
    private Vector3 grapplePoint;           // 그래플링 위치
    private bool isGrappling = false;       // 그래플링 가능 여부

    void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 우클릭으로 그래플
            StartGrapple();

        if (Input.GetMouseButtonUp(0))   // 버튼 떼면 해제
            StopGrapple();

        // 라인 렌더러 업데이트
        if (isGrappling)
        {
            lineRenderer.SetPosition(0, transform.position);    // 이을 선에 처음 지점
            lineRenderer.SetPosition(1, grapplePoint);          // 이을 선에 마지막 지점
        }
    }

    void FixedUpdate()
    {
        if (isGrappling)
        {
            Vector3 direction = (grapplePoint - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, grapplePoint);

            // 일정 거리 이상 가까워지면 자동 해제
            if (distance < 2f)
            {
                StopGrapple();
                return;
            }

            // 물리적으로 당기는 힘 적용
            rb.velocity = direction * pullSpeed;
        }
    }

    void StartGrapple()
    {
        //Ray : 시작점과 방향을 가진 3D 공간의 “광선(선)”
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxGrappleDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            isGrappling = true;

            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);

            Debug.Log($"그래플링 성공! 목표: {grapplePoint}");
        }
    }

    void StopGrapple()
    {
        isGrappling = false;
        lineRenderer.enabled = false;
    }
}

/*
LineRenderer란 ?
private Rigidbody rb; 이 코드는 이 스크립트를 가지고 있는 거에 대한 Rigidbody 를 말하는 거지?

*/