using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabWindow : MonoBehaviour
{

    public GameObject grabbableObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")
        {
            isInBox = true;
        }
    }
    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")
        {
            isInBox = false;
        }
    }
}


public bool isInBox;
 
void Update(){
    if(isInBox){
        Debug.Log("Found in box!");
    } else {
        Debug.Log("Not in box!");
    }
}
 
