using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 10f;
    public LayerMask layerMask;
    public int JumpCount = 2;
    public float groundCheckDistance = 0.1f;

    private int jumpCount;
    private bool iGround;
    private bool iJumpCheck;
    private Rigidbody rb;

    void Awake()
    {

        rb.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (iGround || jumpCount > 0))
        {
            Jump();
        }      
    }
    
    void FixedUpdate()
    {
        GroundCheck();
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCount--;

        // if (isGrounded)
        // {
        //     currentJumpCount = maxJumpCount - 1;  // 바닥에 있을 때는 한 번만 점프 가능
        // }
    }

    void GroundCheck()
    {
        iGround = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, layerMask);

        // if (isGrounded)
        // {
        //     currentJumpCount = maxJumpCount; // 바닥에 있을 때는 점프 횟수 리셋
        // }
    }
}
