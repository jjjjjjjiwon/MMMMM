using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f; // 속도
    public Vector3 GetMoveInput() => moveInput; // 방향 넘기기
    public Camera playerCamera; // 카메라 받기

    private Rigidbody rb;
    private Vector3 moveInput;  // 방향
    private float xRotation = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 초기화
    }

    void Update()
    {
        HandleInput();
        HandleLook();
    }

    // Rigidbody는 FixedUpdate 에서
    void FixedUpdate()
    {
        MovePlayer();
    }

    // 🔹 입력 처리
    void HandleInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveInput = new Vector3(h, 0f, v).normalized;
    }

    // 🔹 이동
    public void MovePlayer()
    {
        Vector3 moveDir = transform.TransformDirection(moveInput); // 월드 좌표로
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime); // Rigidbody에서의 이동
    }

    // 🔹 회전
    public void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * 2f; // *2는 감도
        float mouseY = Input.GetAxis("Mouse Y") * 2f;

        xRotation -= mouseY; // 마우스를 위로 올리면 카메라는 아래로
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 카메라를 좌우 제한

        if (playerCamera != null)
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // 플레이어 Y축 회전에 따라 카메라도 같이 돌아가지만, 상하 회전은 카메라만 독립적으로 처리

        transform.Rotate(Vector3.up * mouseX); // 플레이어 좌우 회전
    }

}
