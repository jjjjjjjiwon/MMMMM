using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookShooter : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public static bool shoot;
    private float linesize = 50f;
    private float currentposition = 0.0f;
    private RaycastHit hit;
    public static Vector3 hitPoint = Vector3.zero;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.enabled = false;
        //Debug.Log("LineRenderer 준비 완료!");
    }

    void OnEnable()
    {
        GrapplingInputHandler.OnShootPressed += TestFunction;
    }
    void OnDisable()
    {
        GrapplingInputHandler.OnShootPressed -= TestFunction;
    }

    void TestFunction()
    {
        //print("델리게이트 작동함!");  // Debug.Log 대신 print 사용
        shoot = !shoot;
        //print("shoot 값: " + shoot);
        if (shoot)
        {
            lineRenderer.enabled = true;
            //print("LineRenderer 켜짐");
        }
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     shoot = !shoot;
        //     if (shoot)
        //     {
        //         lineRenderer.enabled = true; // 발사할 때만 켜기
        //     }
        // }

        if (shoot)
        {
            if (currentposition <= linesize && hitPoint == Vector3.zero)
            {
                currentposition += 0.3f;
            }

            if (Physics.Raycast(transform.position, transform.forward, out hit, currentposition))
            {
                hitPoint = hit.point;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
                //Debug.Log("hit");
            }

            else if (currentposition <= linesize)
            {
                hitPoint = Vector3.zero;
                Vector3 lineEnd = transform.position + transform.forward * currentposition;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, lineEnd);
                //Debug.Log("shoot");
            }
        }
        else
        {
            if (currentposition > 0)
            {
                currentposition -= 0.5f; // 발사보다 빠르게 회수
                Vector3 lineEnd = transform.position + transform.forward * currentposition;

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, lineEnd);
                //Debug.Log("collect");

            }
            if (currentposition <= 0f)
            {
                currentposition = 0f;
                lineRenderer.enabled = false; // 선 꺼줌
                hitPoint = Vector3.zero;
            }
        }

    }
}
