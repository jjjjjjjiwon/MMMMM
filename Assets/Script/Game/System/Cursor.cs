using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public RaycastDebugger raycastDebugger; // 기존 레이캐스트 스크립트
    public GameObject hitPointPrefab;
    public LayerMask targetLayer;
    public float rayDistance = 100f;
    
    private GameObject currentMarker;

    void Update()
    {
        if (raycastDebugger == null)
        {
            Debug.LogWarning("⚠️ RaycastDebugger가 연결되지 않았습니다!");
            return;
        }

        Ray ray = raycastDebugger.GetViewRay();

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, targetLayer))
        {
            Vector3 hitPoint = hit.point;

            if (hitPointPrefab != null)
            {
                if (currentMarker == null)
                {
                    currentMarker = Instantiate(hitPointPrefab, hitPoint, Quaternion.identity);
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
