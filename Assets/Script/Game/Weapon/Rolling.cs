using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour
{
    public float rollingSpeed = 720;
    public float rollingStopLength;
    


    private bool isRolling;
    private Rigidbody rb;
    private Grapple grapple;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grapple = FindObjectOfType<Grapple>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && grapple.IsGrappling)
        {
            StartRolling();
        }

        // 그래플 중단 시 자동 정지
        if (!grapple.IsGrappling && isRolling)
        {
            StopRolling();
        }
    }

    void FixedUpdate()
    {
        if (!isRolling) return;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rollingSpeed * Time.fixedDeltaTime, rollingSpeed * Time.fixedDeltaTime));
        
        Debug.Log("Rolling");
        
    }

    void StartRolling()
    {   
        isRolling = true;
    }

    void StopRolling()
    {
        isRolling = false;
    }
    
}
