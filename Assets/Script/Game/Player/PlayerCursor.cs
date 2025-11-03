using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask aimLayer;
    public Transform aimMarkerPrefab;

    private Transform aimMarkerInstance;

    public bool IsAiming { get; private set; }
    public Vector3 AimPoint { get; private set; }
    public Vector3 AimDirection { get; private set; }

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (aimMarkerPrefab != null)
        {
            aimMarkerInstance = Instantiate(aimMarkerPrefab);
            aimMarkerInstance.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // 우클릭으로 조준 모드 전환
        if (Input.GetMouseButtonDown(1))
        {
            IsAiming = true;
            if (aimMarkerInstance != null)
                aimMarkerInstance.gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            IsAiming = false;
            if (aimMarkerInstance != null)
                aimMarkerInstance.gameObject.SetActive(false);
        }

        if (IsAiming && mainCamera != null)
        {
            // 🎯 마우스 위치 대신 화면 중앙 기준으로 Ray 쏘기
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Ray ray = mainCamera.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, aimLayer))
            {
                AimPoint = hit.point;
                AimDirection = (AimPoint - mainCamera.transform.position).normalized;

                if (aimMarkerInstance != null)
                {
                    aimMarkerInstance.position = AimPoint;
                    aimMarkerInstance.LookAt(mainCamera.transform); // 마커가 카메라를 향하도록
                }
            }
            else
            {
                // 맞은 게 없을 때는 카메라 앞쪽 100m 지점 표시
                AimPoint = ray.origin + ray.direction * 100f;
                AimDirection = ray.direction;

                if (aimMarkerInstance != null)
                {
                    aimMarkerInstance.position = AimPoint;
                    aimMarkerInstance.LookAt(mainCamera.transform);
                }
            }
        }
    }
}
