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
        if (other != null)
        {
            Obstacle potentialComponent = other.GetComponent<Obstacle>();
    
            if (potentialComponent != null && potentialComponent.isOnTrack)
            {
                grabbableObject = other.gameObject;
            }
        }
    }

}
 
