using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;           // 카메라가 따라갈 플레이어 Transform
    public Vector3 offset = new Vector3(0, 2, -10); // 플레이어 위치 기준 카메라 오프셋 (높이 2, 뒤로 10)
    public float mouseSensitivity = 2f; // 마우스 감도 (카메라 회전 속도 조절)
    public float rotationSmooth = 5f;   // 카메라 회전 부드럽게 보간할 때 사용 가능 (지금은 사용 안됨)
public Camera GetCamera()
{
    return GetComponent<Camera>();
}

    private float pitch = 0f;   // 카메라 상하 회전 각도 (X축 회전)
    private float yaw = 0f;     // 카메라 좌우 회전 각도 (Y축 회전)

    void Awake()
    {
        // 플레이어가 연결되지 않은 경우, 태그가 "Player"인 오브젝트 찾아서 자동 연결
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        // 플레이어가 없으면 동작하지 않음
        if (player == null) return;

        // 1️⃣ 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; // 좌우 이동
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; // 상하 이동

        // 2️⃣ 누적 회전값 업데이트
        yaw += mouseX;       // 마우스 좌우 이동에 따라 카메라 Y축 회전 증가
        pitch -= mouseY;     // 마우스 상하 이동에 따라 카메라 X축 회전 감소 (마우스 위로 올리면 위로 보기)
        pitch = Mathf.Clamp(pitch, -35f, 60f); // 상하 회전 제한 (위 60도, 아래 -35도)

        // 3️⃣ 최종 카메라 회전 계산
        Quaternion camRot = Quaternion.Euler(pitch, yaw, 0); // X=pitch, Y=yaw, Z=0

        // 4️⃣ 카메라 위치 계산: 플레이어 위치 + 회전 적용된 오프셋
        transform.position = player.position + camRot * offset;

        // 5️⃣ 카메라 회전 적용
        transform.rotation = camRot;
    }
}
