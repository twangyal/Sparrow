using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.SceneManagement;

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
    bool nearNPC = false;
    public GameObject NPC;
    

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

        if(nearNPC && Input.GetKeyDown(KeyCode.E))
        {
            NPC.GetComponent<Talk>().Recording();
            NPC.GetComponent<ObjectRequest>().StartConversation("English");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        cam.GetComponent<PlayerLook>().TriggerEnter(other); //when you enter the collider
        if(other.tag == "NPC")
        {
            nearNPC = true;
            NPC = other.gameObject;
        }
        if(other.tag == "DoorIn"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(other.tag == "DoorOut"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        cam.GetComponent<PlayerLook>().TriggerExit(other);//when you exit the collider
        if(other.tag == "NPC")
        {
            nearNPC = false;
            NPC = null;
        }
    }
}

