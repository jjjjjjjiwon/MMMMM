using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Vector3 MoveInput
    {
        get { return moveInput; }
    }

    [Header("Mouse Look")]
    public Camera playerCamera;
    public float mouseSensitivity = 2f;
    private float xRotation = 0f;

    private Rigidbody rb;
    private Vector3 moveInput;

    void Awake() => rb = GetComponent<Rigidbody>();

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 잠금
        Cursor.visible = false;
    }

    void Update()
    {
        // 이동 입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveInput = new Vector3(h, 0f, v).normalized;

        // 마우스 회전
        HandleLook();
    }

    void FixedUpdate()
    {
        // 이동
        Vector3 moveDir = transform.TransformDirection(moveInput); // 카메라가 아닌 플레이어 기준 이동
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}



// .fixedDeltaTime이란?
// FixedUpdate()가 한 번 실행될 때 걸리는 시간 (물리 프레임 간격)
// 일정한 간격으로 보정하기 위해

// Time.time >= nextDashTime 은 
// Time.time : 게임이 시작된 뒤 흐른 총 시간(초)
// nextDashTime : 다음 대쉬 가능한 시간
// nextDashTime = Time.time + dashCooldown 이런 식으로 동작해
// 현재 시간 + 대쉬 가능한 시간으로해
// 대쉬가 가능한 시간을 찾는

// dashTime -= Time.fixedDeltaTime;
// dashTime = dashDuration; 이런 식으로 동작
// 대쉬 중인 시간을 카운트다운하는 코드
