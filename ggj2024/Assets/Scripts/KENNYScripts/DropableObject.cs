using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DropableObject : MonoBehaviour
{
    private PlayerInputActions _input;

    private SpringJoint _springJoint;

    private void Start()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();
        _input.Player2.Enable();

        _input.Player.Drop.performed += Drop_performed;
        _input.Player2.Drop.performed += Drop_performed1;
        _springJoint = GetComponent<SpringJoint>();
    }

    private void Drop_performed1(InputAction.CallbackContext obj)
    {
        if (_springJoint != null)
        {
            _springJoint.breakForce = 0;
        }
    }

    private void Drop_performed(InputAction.CallbackContext obj)
    {
        if (_springJoint != null)
        {
            _springJoint.breakForce = 0;
        }
    }

    private void Update()
    {
        
    }


}
