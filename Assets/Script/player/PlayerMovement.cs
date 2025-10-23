using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 20f; // 대시 속도
    public float dashDuration = 0.2f; // 대시 지속시간 , 지속 시간으로 거리를 
    public float dashColldown = 1f; // 대시 쿨타임

    private Rigidbody rb;
    private Vector3 moveInput;
    private bool isDashing = false; // 대쉬 가능 여부
    private float dashTime; // ?
    private float nextDashTime = 0f;



    void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveInput = new Vector3(h, 0f, v).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextDashTime)
        {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.MovePosition(rb.position + moveInput * dashSpeed * Time.fixedDeltaTime);
            dashTime -= Time.fixedDeltaTime;

            if (dashTime <= 0f)
            {
                isDashing = false;
            }
        }
        else
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void StartDash()
    {
        if (moveInput == Vector3.zero) return; // 데쉬 방향 없으면 무시

        isDashing = true;
        dashTime = dashDuration;
        nextDashTime = Time.time + dashColldown;
        Debug.Log("대쉬 시작!");
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
