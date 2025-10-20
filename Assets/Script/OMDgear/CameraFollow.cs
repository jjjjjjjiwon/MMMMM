using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2.0f;
    
    // 카메라 거리 (추가)
    public float normalDistance = 5f;   // 평소 거리
    public float aimDistance = 2f;      // 조준 시 거리
    public float zoomSpeed = 10f;       // 줌 속도
    
    private float currentDistance;      // 현재 거리
    private float rotationX = 0f;
    private float rotationY = 0f;
    
    void Start()
    {
        currentDistance = normalDistance;
    }
    
    void Update()
    {
        // 마우스 입력 (기존)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);
        
        // 거리 변경 추가
        float targetDistance = (WireGun.Instance != null && WireGun.Instance.isAiming) 
                       ? aimDistance 
                       : normalDistance;
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * zoomSpeed);
        
        // 회전 및 위치 (기존, 거리만 변경)
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 offset = rotation * new Vector3(0, 2, -currentDistance);
        
        transform.position = player.position + offset;
        transform.LookAt(player);
    }
}
