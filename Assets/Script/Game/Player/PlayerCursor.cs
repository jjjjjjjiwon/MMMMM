using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask aimLayer;
    public Transform aimMarkerPrefab; // 조준점 마커 프리팹
    private Transform aimMarkerInstance;

    public bool IsAiming { get; private set; }
    public Vector3 AimPoint { get; private set; }
    public Vector3 AimDirection { get; private set; }

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        aimMarkerInstance = Instantiate(aimMarkerPrefab);
        aimMarkerInstance.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 우클릭 시작
        {
            IsAiming = true;
            aimMarkerInstance.gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1)) // 우클릭 종료
        {
            IsAiming = false;
            aimMarkerInstance.gameObject.SetActive(false);
        }

        if (IsAiming)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, aimLayer))
            {
                AimPoint = hit.point;
                AimDirection = (AimPoint - transform.position).normalized;
                aimMarkerInstance.position = AimPoint;
            }
            else
            {
                // 히트 못하면 먼 거리 조준 (예: 카메라 앞 100m)
                AimPoint = ray.origin + ray.direction * 100f;
                AimDirection = ray.direction;
                aimMarkerInstance.position = AimPoint;
            }
        }
    }
}


