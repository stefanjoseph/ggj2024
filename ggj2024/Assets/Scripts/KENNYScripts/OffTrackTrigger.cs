using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffTrackTrigger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1Object" ||  other.tag == "Player2Object")
        {
            //other.GetComponent<TreddyObject>().enable = false;
            other.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
