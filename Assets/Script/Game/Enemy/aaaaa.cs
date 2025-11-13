using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class aaaaa : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask grappleLayer;
    public LineRenderer lineRenderer;
    public float pullForce = 25f;
    public float maxDistance = 100f;
    public float minRopeLength = 5f;     // ✅ 최소 줄 길이
    public float maxRopeLength = 50f;    // ✅ 최대 줄 길이
    public float ropeAdjustSpeed = 10f;  // ✅ 줄 조절 속도
    public bool IsGrappling => isGrappling;

    private float ropeLength;
    private bool isGrappling = false;
    private Vector3 grapplePoint;
    private Rigidbody rb;
    private RaycastDebugger raycastDebugger;
    private Cursor cursor;
    private PlayerDash playerDash;

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

        // ✅ 줄 길이 조절 (마우스 휠)
        if (isGrappling)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel"); // 위/아래 스크롤
            if (Mathf.Abs(scroll) > 0.01f)
            {
                ropeLength -= scroll * ropeAdjustSpeed;
                ropeLength = Mathf.Clamp(ropeLength, minRopeLength, maxRopeLength);
            }
        }

        if (isGrappling && lineRenderer != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    void FixedUpdate()
    {
        if (!isGrappling) return;

        Vector3 toGrapple = grapplePoint - transform.position;
        float distance = toGrapple.magnitude;
        Vector3 dir = toGrapple.normalized;

        // 목표점으로 당기는 힘
        rb.AddForce(dir * pullForce, ForceMode.Force);

        // 스윙 방향 (좌우 입력)
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 swingDir = Vector3.Cross(dir, Vector3.up).normalized * horizontal;
        rb.AddForce(swingDir, ForceMode.Force);

        // ✅ 줄 길이 유지 (너무 멀어지면 되돌림)
        if (distance > ropeLength)
        {
            Vector3 correction = dir * (distance - ropeLength);
            rb.MovePosition(transform.position + correction);
        }
    }

    void StartGrapple()
    {
        Ray ray = raycastDebugger.GetViewRay();
        float sphereRadius = (cursor != null) ? cursor.sphereRadius : 1.5f; // ✅ 커서 반경 참조

        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit, maxDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            isGrappling = true;
            ropeLength = Vector3.Distance(transform.position, grapplePoint);

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
