using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10.0f;
    public float jumpForce = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        rb.freezeRotation = true; // 중요: 캐릭터가 넘어지지 않게

        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        // 회전
        // if (Input.GetKey(KeyCode.Q)) transform.Rotate(0, -90 * Time.deltaTime, 0);
        // if (Input.GetKey(KeyCode.E)) transform.Rotate(0, 90 * Time.deltaTime, 0);

        // 이동 로직

        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveDir += transform.forward;
        if (Input.GetKey(KeyCode.S)) moveDir -= transform.forward;

        if (Input.GetKey(KeyCode.A)) moveDir -= transform.right;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) transform.Rotate(0, -90 * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.D)) moveDir += transform.right;
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)) transform.Rotate(0, 90 * Time.deltaTime, 0);

        rb.AddForce(moveDir.normalized * speed);


        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


}
