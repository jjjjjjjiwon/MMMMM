using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public RaycastDebugger raycastDebugger; // 기존 레이캐스트 스크립트
    public GameObject hitPointPrefab;
    public LayerMask targetLayer;
    public float rayDistance = 100f;
    public float sphereRadius = 2f; // 감지 범위
    
    private GameObject currentMarker;

    void Update()
    {
        if (raycastDebugger == null)
        {
            Debug.LogWarning("⚠️ RaycastDebugger가 연결되지 않았습니다!");
            return;
        }

        Ray ray = raycastDebugger.GetViewRay();

        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit, rayDistance, targetLayer)) // 점이 아니라 구체 로 감지
        {
            Vector3 hitPoint = hit.point;

            if (hitPointPrefab != null)
            {
                if (currentMarker == null)
                {
                    currentMarker = Instantiate(hitPointPrefab, hitPoint, Quaternion.identity);
                    //currentMarker.transform.localScale = Vector3.one * 3f; 
                }
                else
                {
                    currentMarker.transform.position = hitPoint;
                }
            }
        }
        else
        {
            if (currentMarker != null)
                Destroy(currentMarker);
        }

        // 디버그용 (씬 뷰에서 레이 확인)
        raycastDebugger.DrawDebugRay(Color.yellow);
    }
    }
