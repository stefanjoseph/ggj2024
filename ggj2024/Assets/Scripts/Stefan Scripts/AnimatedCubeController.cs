using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCubeController : MonoBehaviour
{
    public float Y_POS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x , Y_POS, this.transform.position.z);
    }
}
