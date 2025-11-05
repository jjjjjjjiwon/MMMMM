using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    [Header("References")]
    public LayerMask aimLayer;               // 조준할 레이어 (Raycast가 감지할 레이어)
    public Transform aimMarkerPrefab;        // 조준 마커 프리팹 (마커를 생성할 프리팹)
    public bool IsAiming { get; private set; } // 조준 모드 상태 (true: 조준, false: 비조준)
    public Vector3 AimPoint { get; private set; } // 조준 지점 (Raycast가 맞은 지점)
    public Vector3 AimDirection { get; private set; } // 카메라에서 조준 지점까지의 방향 벡터

    private Transform aimMarkerInstance;    // 조준 마커의 인스턴스 (실제로 씬에 생성된 객체)

    // 🔹 Start: 초기화 함수
    void Start()
    {
        // aimMarkerPrefab이 설정된 경우 마커를 씬에 생성
        if (aimMarkerPrefab != null)
        {
            aimMarkerInstance = Instantiate(aimMarkerPrefab);  // 마커 프리팹을 인스턴스화
            aimMarkerInstance.gameObject.SetActive(false);    // 초기에는 비활성화
        }
    }

    // 🔹 Update: 매 프레임마다 실행되는 함수
    void Update()
    {
        HandleAimingInput();  // 우클릭 입력에 따른 조준 모드 전환
        UpdateAim();          // 조준 상태일 때 AimPoint와 AimDirection을 업데이트
    }

    // 🔹 우클릭으로 조준 모드 전환
    void HandleAimingInput()
    {
        if (Input.GetMouseButtonDown(1)) // 우클릭 시 조준 시작
        {
            IsAiming = true;
            if (aimMarkerInstance != null) aimMarkerInstance.gameObject.SetActive(true); // 마커 활성화
        }

        if (Input.GetMouseButtonUp(1)) // 우클릭을 떼면 조준 종료
        {
            IsAiming = false;
            if (aimMarkerInstance != null) aimMarkerInstance.gameObject.SetActive(false); // 마커 비활성화
        }
    }

    // 🔹 조준 지점 계산 및 마커 위치 갱신
    void UpdateAim()
    {
        // 조준 모드일 때만 실행
        if (!IsAiming) return;

        Camera cam = FindObjectOfType<FollowCamera>().GetCamera(); // FollowCamera에서 카메라 가져오기
        if (cam == null) return;  // 카메라가 없으면 반환

        // 화면 중앙의 좌표를 기준으로 Ray를 쏘기
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = cam.ScreenPointToRay(screenCenter);  // 카메라에서 화면 중앙을 기준으로 Ray 쏘기

        // 레이캐스트를 쏘아서 맞은 지점 계산
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, aimLayer)) // 레이캐스트로 맞은 지점이 있는지 체크
        {
            AimPoint = hit.point;  // 맞은 지점 저장
            AimDirection = (AimPoint - cam.transform.position).normalized;  // 카메라에서 조준 지점까지의 방향 계산

            // 마커 갱신
            if (aimMarkerInstance != null)
            {
                aimMarkerInstance.position = AimPoint;  // 마커를 맞은 지점에 위치시킴
                aimMarkerInstance.LookAt(cam.transform); // 마커가 카메라를 바라보도록 설정
            }
        }
        else
        {
            // 맞은 지점이 없을 때는 마커를 비활성화
            if (aimMarkerInstance != null)
            {
                aimMarkerInstance.gameObject.SetActive(false); // 마커 비활성화
            }
        }
    }
    
}
