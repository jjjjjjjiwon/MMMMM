using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 20f; // 대시 속도
    public float dashDuration = 0.2f; // 대시 지속시간 , 지속 시간으로 거리를 
    public float dashColldown = 1f; // 대시 쿨타임
    public bool IsDashing => isDashing; // 다른 스크립트에서 대쉬 중인가 검사를 위해
    public Camera playerCamera;

    private Rigidbody rb;
    private bool isDashing = false; // 대쉬 가능 여부
    private float dashTime; // ?
    private float nextDashTime = 0f;
    private PlayerMovement playerMovement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextDashTime)
        {
            StartDash();
        }
    }

    void FixedUpdate()
{
    if (!isDashing) return;

    Vector3 dashDir = Camera.main.transform.TransformDirection(playerMovement.MoveInput);
    if (dashDir == Vector3.zero) return;

    rb.velocity = dashDir * dashSpeed; // 순간 속도 변경

    dashTime -= Time.fixedDeltaTime;
    if (dashTime <= 0f)
    {
        isDashing = false;
        rb.velocity = Vector3.zero; // 대쉬 종료 시 속도 초기화
    }
}

    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        nextDashTime = Time.time + dashColldown;
        Debug.Log("대쉬 시작!");

    }
    
}
