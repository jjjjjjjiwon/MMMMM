using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // 플레이어
    public Transform player;
    // 마우스 감도
    public float mouseSensitivity = 2.0f;

    // 카메라 위치
    private Vector3 offset;
    // X축 각도
    private float rotationX = 0f;
    // Y축 각도
    private float rotationY = 0f;


    void Update()
    {
        // 마우스 입력
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 각도 계산
        rotationY += mouseX;
        rotationX -= mouseY;

        // X축 각도 제한, 좌우는 상관 없는데 상하 제한 안두면 
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);

        // 회전
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

        // 카메라 위치
        offset = rotation * new Vector3(0, 2, -5);

        // 카메라 플레이어 따라가게
        transform.position = player.position + offset;
        transform.LookAt(player);

        //Debug.Log($"mouseX: {mouseX}, mouseY: {mouseY}, rotationX: {rotationX}, rotationY: {rotationY}");
    }
}
