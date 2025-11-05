using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashSpeed = 20f;       // 대시 속도
    public float dashDuration = 0.2f;   // 대시 지속시간
    public float dashCooldown = 1f;     // 대시 쿨타임

    public bool IsDashing => isDashing; // 외부에서 대시 중인지 확인용

    private Rigidbody rb;
    private bool isDashing = false;     // 현재 대시 중인지 여부
    private float dashTime = 0f;        // 대시 남은 시간
    private float nextDashTime = 0f;    // 다음 대시 가능 시간

    private PlayerMovement playerMovement; // 이동 입력을 가져오기 위한 참조

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        HandleDashInput();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            PerformDash();
        }
    }

    // 🔹 입력 처리
    private void HandleDashInput()
    {
        // LeftShift 입력 & 쿨타임 체크
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextDashTime)
        {
            StartDash();
        }
    }

    // 🔹 대시 시작
    private void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        nextDashTime = Time.time + dashCooldown;

        Debug.Log("대쉬 시작!");
    }

    // 🔹 대시 진행
    private void PerformDash()
    {
        // 플레이어 입력 기준 방향 가져오기
        Vector3 dashDir = Camera.main.transform.TransformDirection(playerMovement.GetMoveInput());

        // Y축 성분 제거 → 백대시 시 점프 방지
        dashDir.y = 0f;
        dashDir.Normalize();

        if (dashDir.sqrMagnitude < 0.01f)
        {
            // 입력이 거의 없으면 대시 종료
            EndDash();
            return;
        }

        // 순간 속도로 이동
        rb.velocity = dashDir * dashSpeed;

        // 대시 시간 감소
        dashTime -= Time.fixedDeltaTime;
        if (dashTime <= 0f)
        {
            EndDash();
        }
    }

    // 🔹 대시 종료
    private void EndDash()
    {
        isDashing = false;

        // 대시 종료 시 속도 초기화
        rb.velocity = Vector3.zero;
        Debug.Log("대쉬 종료!");
    }
}
