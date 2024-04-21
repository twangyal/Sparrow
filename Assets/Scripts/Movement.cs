using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class movement : MonoBehaviour
{
    public float speed = 5.0f;
    public CharacterController controller;
    public float gravity = -9.81f;
    public float jumpHeight = 3.0f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public GameObject cam;
    Vector3 velocity;
    bool isGrounded;
    

    void Update()
    {
        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        //move
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        cam.GetComponent<PlayerLook>().TriggerEnter(other); //when you enter the collider
    }
    private void OnTriggerExit(Collider other)
    {
        cam.GetComponent<PlayerLook>().TriggerExit(other);//when you exit the collider
    }
}

