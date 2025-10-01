using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    CharacterController controller;
    public float speed = 10.0f;

    private Vector3 velocity;
    public float gravity = -9.81f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(direction * speed * Time.deltaTime);

        // Vector3 move = transform.right * Input.GetAxis("Horizontal") +
        //                transform.forward * Input.GetAxis("Vertical");
        Vector3 move = Camera.main.transform.right * Input.GetAxis("Horizontal") +
                    Camera.main.transform.forward * Input.GetAxis("Vertical");

        controller.Move(move * speed * Time.deltaTime);

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // 바닥에 닿으면 y속도 초기화
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;
    }
}
