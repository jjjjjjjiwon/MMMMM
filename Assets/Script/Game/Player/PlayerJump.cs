using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f;
    public int maxJumps = 2;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private int jumpCount;

    void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        if (IsGrounded())
        {
            jumpCount = 0;
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
