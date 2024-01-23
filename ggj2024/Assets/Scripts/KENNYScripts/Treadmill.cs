using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObjectDrop")
        {
            other.transform.parent = gameObject.transform;
            Debug.Log("Object is Parented to the track");
        }
    }
}
