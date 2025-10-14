using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingMover : MonoBehaviour
{
    public static bool Move;
    public float MoveSpeed = 200f;

    private Vector3 targetPos;
    private Vector3 swingPoint;      // 와이어 끝점
    private float wireLength;         // 와이어 길이
    private Rigidbody rb;

    void OnEnable()
    {
        GrapplingInputHandler.OnMovePressed += MoveFunction;
    }
    void OnDisable()
    {
        GrapplingInputHandler.OnMovePressed -= MoveFunction;
    }

    void MoveFunction()
    {
        if (GrapplingHookShooter.shoot && GrapplingHookShooter.hitPoint != Vector3.zero)
        {
            swingPoint = GrapplingHookShooter.hitPoint;
            wireLength = Vector3.Distance(transform.position, swingPoint);
            Move = true;
        }
        else
        {
            Move = false;
            //print("발사 상태가 아니거나 충돌 지점이 없음");
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
{
    if (!Move) return;

    // 1. hook 방향 계산
    Vector3 direction = (swingPoint - transform.position).normalized;

    // 2. 기존 y 속도 보존 (점프 방지 핵심)
    Vector3 currentVelocity = rb.velocity;
    Vector3 newVelocity = new Vector3(direction.x, currentVelocity.y, direction.z);

    // 3. 이동 적용
    rb.velocity = newVelocity * MoveSpeed;

    // 4. 도착 체크
    if (Vector3.Distance(transform.position, swingPoint) < wireLength * 0.1f)
    {
        Move = false;
        rb.velocity = Vector3.zero;
    }
}
}
