using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;              // 따라갈 대상
    public Vector3 offset = new Vector3(0, 5, -10); // 초기 카메라 위치 오프셋
    public float mouseSensitivity = 5f;   // 마우스 감도
    public float pitchMin = -20f;         // 카메라 상하 최소
    public float pitchMax = 80f;          // 카메라 상하 최대

    private float yaw = 0f;   // 좌우 회전
    private float pitch = 25f; // 상하 회전

    void LateUpdate()
    {
        if (player == null) return;

        // 1️⃣ 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 2️⃣ 회전값 업데이트
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        // 3️⃣ 카메라 회전 적용
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.rotation = rotation;

        // 4️⃣ 카메라 위치 계산
        transform.position = player.position + rotation * offset;

        // ✅ 이제 LookAt을 사용하지 않아도 pitch가 제대로 적용됨
    }
}
