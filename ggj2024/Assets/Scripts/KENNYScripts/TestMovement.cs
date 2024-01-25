using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMovement : MonoBehaviour
{
    private PlayerInputActions _input;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _smackSpeed = 15f;

    [SerializeField] private float _slapRate = 6f;
    private float _canSlap = -1f;

    private bool _isSmacking;
    private bool _playerCanMove;

    private void Start()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();

        _playerCanMove = true;
    }

    private void Update()
    {
        var move = _input.Player.Movement.ReadValue<Vector2>();

        if (_playerCanMove == true)
        {
            transform.Translate(move * _speed * Time.deltaTime);

        }

        if (Input.GetKeyDown(KeyCode.L) && Time.time > _canSlap)
        {
            Debug.Log("slap rate is working");
            _isSmacking = true;
            _playerCanMove = false;
        }

        HandSmack();

    }


    private void HandSmack()
    {
        
        Vector3 pos = transform.position;
        pos.x = -6;

        if (_isSmacking == true)
        {
            transform.Translate(Vector3.right * _smackSpeed * Time.deltaTime);

        }

        if (transform.position.x >= 5)
        {
            transform.position = pos;
            
        }

        if (transform.position.x <= -6f)
        {
            //Cool Down rate for the next available slap. 
            //Can be adjusted in the Inspector
            _canSlap = Time.time + _slapRate;

            _isSmacking = false;
            _playerCanMove = true;
        }
    }


}
    

