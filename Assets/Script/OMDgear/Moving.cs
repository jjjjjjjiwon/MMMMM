using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Rigidbody형식의 이동
public class Moving : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10.0f;
    public float jumpForce = 5.0f;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {

        Vector3 move = Camera.main.transform.right * Input.GetAxis("Horizontal") +
                   Camera.main.transform.forward * Input.GetAxis("Vertical");
    
    rb.velocity = new Vector3(move.x * speed, rb.velocity.y, move.z * speed);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    }


}
