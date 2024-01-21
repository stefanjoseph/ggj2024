using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMovement : MonoBehaviour
{
    private PlayerInputActions _input;

    private float _speed = 5f;

    private void Start()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();
    }

    private void Update()
    {
        var move = _input.Player.Movement.ReadValue<Vector2>();

        transform.Translate(move * _speed * Time.deltaTime);
    }

    
}
