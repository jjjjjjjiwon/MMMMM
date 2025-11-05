using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f; // 점프 높이
    public int maxJumps = 2;    // 점프 횟수
    public Transform groundCheck;   // 땅 인가 체크할 오브젝트
    public LayerMask groundLayer;   // 땅 인가?

    private Rigidbody rb;
    private int jumpCount; // 점프 횟수 세기

    void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        if (IsGrounded())
        {
            jumpCount = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            jumpCount++;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
    }
}
