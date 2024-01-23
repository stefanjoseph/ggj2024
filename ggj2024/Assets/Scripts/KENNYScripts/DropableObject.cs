using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropableObject : MonoBehaviour
{
    private SpringJoint _springJoint;

    private void Start()
    {
        _springJoint = GetComponent<SpringJoint>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_springJoint != null)
            {
                _springJoint.breakForce = 0;
            }
        }
    }


}
