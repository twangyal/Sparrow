using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public Material highlightMaterial;
    public Material originalMaterial;
    public TextMeshProUGUI itemName;
    public Transform cameraToFind;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cameraToFind = GameObject.Find("Main Camera").transform;
        itemName.text = transform.name;
        itemName.enabled = false;
        var outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
        outline.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        itemName.transform.LookAt(cameraToFind);
        itemName.transform.Rotate(0,180,0);
        

    
    }
    public void LookingAt()
    {
        var outline = GetComponent<Outline>();
        outline.enabled = true;
        itemName.enabled = true;
        itemName.transform.LookAt(cameraToFind);
        itemName.transform.Rotate(0,180,0);
    }
    public void NotLookingAt()
    {
        var outline = GetComponent<Outline>();
        outline.enabled = false;
        itemName.enabled = false;
        
    }
    public void pickUp(GameObject playerHand){
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = playerHand.transform.position;
        transform.parent = playerHand.transform;
    }
    public void drop(){
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
}
