using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 10f;  // 이동 속도
    public Vector3 MoveInput => moveInput;
    private Vector3 moveInput;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Player 이동 함수 호출
        MovePlayer();
    }

    void FixedUpdate()
    {
        // Rigidbody를 사용한 이동 처리
        rb.MovePosition(rb.position + moveInput * Speed * Time.fixedDeltaTime);
    }

    void MovePlayer()
    {
        float h = Input.GetAxis("Horizontal");  // 좌우 이동 (A, D, ←, →)
        float v = Input.GetAxis("Vertical");    // 상하 이동 (W, S, ↑, ↓)

        // 이동 벡터 계산
        moveInput = new Vector3(h, 0f, v).normalized; // Y는 0으로 설정하여 XZ 평면에서만 이동하도록 함
    }
}
