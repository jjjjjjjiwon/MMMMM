using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingMover : MonoBehaviour
{
    public static bool Move;
    public float MoveSpeed = 200f;
    private Vector3 targetPos;
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
            targetPos = GrapplingHookShooter.hitPoint;
            Move = !Move;
            //print("이동 시작!");
        }
        else
        {
            Move = false;
            //print("발사 상태가 아니거나 충돌 지점이 없음");
        }
        
    }


    void Update()
    {
        if (Move)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed * Time.deltaTime);

            // 목표 지점에 도달하면 이동 중단
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                Move = false;
                //print("이동 완료!");
            }
            if (GrapplingHookShooter.shoot == false)
            {
                Move = false;
            }
        }
        else
        {
            //print("이동 중지!");
        }
    }
}
