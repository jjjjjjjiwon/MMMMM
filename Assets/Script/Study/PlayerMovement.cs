using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // 이 스크립트를 붙인 GameObject에 Rigidbody가 꼭 있어야 한다는 걸 Unity에게 알려주는 "속성(Attribute)" 
                                      // 이 스크립트를 빈 오브젝트에 붙이면, Unity가 자동으로 Rigidbody 컴포넌트를 추가해줍니다.
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f; // 이동 속도
    public float runSpeed = 10f; // 달리기 속도
    public float jumpForce = 5f; // 점프 힘
    public Transform groundCheck; // 지면 체크
    public float groundCheckRadius = 0.2f; // 지면 범위
    public LayerMask groundLayer; // 지면 레이어

    private Rigidbody rb;
    private Vector3 moveInput; // 이동 위치
    private float currentSpeed; // 현재 속도 저장



    void Awake()
    {
        groundCheck.localPosition = new Vector3(0, -0.2f, 0);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 입력 처리: 프레임마다
        float h = Input.GetAxis("Horizontal"); // A(-1) ↔ D(1) , 좌우 
        float v = Input.GetAxis("Vertical");   // S(-1) ↔ W(1) , 상하 

        // 방향 설정 (X,Z축만 사용)
        moveInput = new Vector3(h, 0, v).normalized; // .normalized : 대각선 속도 유지

        if (Input.GetKey(KeyCode.Space) && IsGround())
        {   
            // AddForce()는 Rigidbody가 가진 힘을 설정하는 함수
            // 힘을 줄 때는 Update()에서 키 입력을 받아 처리해도 문제 없습니다.
            //실제 물리 처리는 **FixedUpdate()**에서 일어나지만, 입력 감지는 Update()에서 하는 게 일반적인 구조입니다.
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        Debug.Log("Grounded: " + IsGround());
        CheckGroundDebug();  // 이 줄 추가
    }

    void FixedUpdate()
    {
        // 실제 물리 이동 처리: 고정 프레임마다
        Vector3 moveVelocity = moveInput * currentSpeed; // 벡터 * 정수 = 방향을 알고 있는 크기 
        Vector3 newPosition = rb.position + moveVelocity * Time.fixedDeltaTime; // Rigidbody를 가지고 있으면 Rigidbody.position을 사용하자
        rb.MovePosition(newPosition); // Rigidbody를 자연스럽게 이동
    }

    private bool IsGround() // 지면에 있는지 체크
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

void CheckGroundDebug()
{
    Collider[] hits = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
    foreach (var hit in hits)
    {
        Debug.Log("💥 닿은 오브젝트: " + hit.gameObject.name);
    }
}
}

