using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f; // 이동 속도
    public float runSpeed = 10f; // 달리기 속도
    public float jumpForce = 5f; // 점프 힘 (3~5 사이로 조정)
    public Transform groundCheck; // 지면 체크
    public float groundCheckRadius = 0.2f; // 지면 범위
    public LayerMask groundLayer; // 지면 레이어

    private Rigidbody rb;
    private Vector3 moveInput; // 이동 위치
    private float currentSpeed; // 현재 속도 저장

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // groundCheck는 Hierarchy에서 수동으로 설정하세요
        // Awake에서 위치를 강제로 변경하지 않습니다
    }

   void Update()
{
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    moveInput = new Vector3(h, 0, v).normalized;

    bool isGrounded = IsGround();
    Debug.Log("Grounded: " + isGrounded); // 바닥에 있을 때 true여야 함
    
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Debug.Log("스페이스 키 눌림!"); // 키 입력 확인
        
        if (isGrounded)
        {
            Debug.Log("점프 실행!"); // 실제 점프 확인
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("지면에 없어서 점프 불가"); // 왜 안 되는지 확인
        }
    }

    currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
}

    void FixedUpdate()
    {
        // 실제 물리 이동 처리: 고정 프레임마다
        Vector3 moveVelocity = moveInput * currentSpeed; // 벡터 * 정수 = 방향을 알고 있는 크기 
        Vector3 newPosition = rb.position + moveVelocity * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private bool IsGround() // 지면에 있는지 체크
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    //지면 체크 시각화
    // void OnDrawGizmos()
    // {
    //     if (groundCheck != null)
    //     {
    //         // 지면에 있으면 초록색, 공중이면 빨간색
    //         Gizmos.color = IsGround() ? Color.green : Color.red;
    //         Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    //     }
    // }
}