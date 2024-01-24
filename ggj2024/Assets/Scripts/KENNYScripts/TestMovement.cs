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

    private Vector3 _lastPosition;
    private Vector3 _currentPosition;

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

        if (Input.GetKeyDown(KeyCode.L))
        {
            _isSmacking = true;
            _playerCanMove = false;
        }



        HandSmack();
    }


    private void HandSmack()
    {
        //Vector3 position = transform.position;
        //float x = position.x;

        //_currentPosition = transform.position;

        Vector3 pos = transform.position;
        pos.x = -6;

        if (_isSmacking == true)
        {
            transform.Translate(Vector3.right * _smackSpeed * Time.deltaTime);

        }

        if (transform.position.x >= 5)
        {
            _isSmacking = false;
            transform.position = pos;
            //if (_isSmacking == false)
            //{
            //    //this.transform.Translate(Vector3.left * _smackSpeed * Time.deltaTime);
            //    //transform.position -= (returnPoint * _smackSpeed * Time.deltaTime); ;

            //    //Vector3 returnToPosition = transform.position - _currentPosition;
            //    //returnToPosition = returnToPosition.normalized;
            //    //transform.position -= returnToPosition * _smackSpeed * Time.deltaTime;

            //    ////This Places it where I want it to be
            //    //transform.position = temp;
            //    Debug.Log("Player is moving left");
            //}

        }

        //if (transform.position == _currentPosition)
        //{
        //    _playerCanMove = true;

        if (transform.position.x <= -6f)
        {
            _playerCanMove = true;

        }
    }


}
    

