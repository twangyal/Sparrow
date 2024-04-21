using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //camera behavior
    public float mouseSensitivity =  100f;
    public Transform playerBody;
    public float xRotation = 0f;
    private GameObject objectLookedAt;
    private GameObject objectHeld;
    public GameObject playerHand;

    private bool pickupAble = false;
    private bool isHolding = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mousex);
        
        //shoot a raycast to see what we are looking at
        Shoot();
        
        //pickup object
        if(Input.GetKeyDown(KeyCode.E)&&pickupAble&&!isHolding)
        {
            objectLookedAt.GetComponent<Interact>().pickUp(playerHand);
            objectHeld = objectLookedAt;
            isHolding = true;
        }
        else if(Input.GetKeyDown(KeyCode.E)&&isHolding)
        {
            objectHeld.GetComponent<Interact>().drop();
            isHolding = false;
        }
    }
    //Check what we are looking at and highlights interactable objects
    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 100)&& 
        !isHolding&&
        hit.transform.tag == "Interactable")
        {
            objectLookedAt=hit.transform.gameObject;
            objectLookedAt.GetComponent<Interact>().LookingAt();
        }
        else{
            if(objectLookedAt != null)
            {
                objectLookedAt.GetComponent<Interact>().NotLookingAt();
            }
        }
    }
    public void TriggerEnter(Collider other)
    {
        if(other.gameObject.Equals(objectLookedAt))
        {    
            
            pickupAble = true;
        }
        else if(!isHolding){
            pickupAble = false;
        
        }
    }
    public void TriggerExit(Collider other)
    {
        pickupAble = false; //when you leave the collider set the canpickup bool to false
    }
}
