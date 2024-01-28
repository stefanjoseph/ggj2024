using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Obstacle : MonoBehaviour
{   
    private Rigidbody mybody;
    // Start is called before the first frame update
    void Start()
    {
        mybody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mybody.velocity = Vector3.forward * -15.0f;
    }
}
