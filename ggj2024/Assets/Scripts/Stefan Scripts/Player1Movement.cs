using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player1Movement : MonoBehaviour
{
    private PlayerInputActions _input;

    [SerializeField] private Image _slapIcon;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _slapSpeed = 15f;

    [SerializeField] private float _slapRate = 6f;
    private float _canSlap = -1f;

    private bool _isSlapping;
    private bool _playerCanMove;

    public BoxCollider constrainer;

    private void Start()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();
        _input.Player.Slap.performed += Drop_performed;

        _playerCanMove = true;
    }

    private void Drop_performed(InputAction.CallbackContext context)
    {
        if (Time.time > _canSlap)
        {
            _isSlapping = true;
            _playerCanMove = false;
        }
    }

    private void Update()
    {
        var move = _input.Player.Movement.ReadValue<Vector3>();

        if (_playerCanMove == true)
        {
            transform.Translate(move * _speed * Time.deltaTime);
        }

        if (Time.time < _canSlap)
        {
            var tempAlpha = _slapIcon.color;
            tempAlpha.a = 0.25f;
            _slapIcon.color = tempAlpha;
        }
        else
        {
            var tempAlpha = _slapIcon.color;
            tempAlpha.a = 1f;
            _slapIcon.color = tempAlpha;
        }

        HandSlap();
    }

    private void LateUpdate()
    {
        ConstrainPlayer();
    }

    private void HandSlap()
    {
        Vector3 pos = transform.position;

        //Commences Hand Slap
        if (_isSlapping == true)
        {
            transform.Translate(Vector3.left * _slapSpeed * Time.deltaTime);
        }

        //Resets Player position when slap ends
        if (transform.position.x <= constrainer.bounds.min.x && _isSlapping == true)
        {
            pos.x = constrainer.bounds.max.x;
            transform.position = pos;
        }

        //Starts the slap cooldown rate and enables the player to freely move again
        if (pos.x >= constrainer.bounds.max.x && _isSlapping == true)
        {
            _canSlap = Time.time + _slapRate;
            _isSlapping = false;
            _playerCanMove = true;
        }
    }

    //Box collider that contsrains the Player movement within the box
    private void ConstrainPlayer()
    {
        Vector3 clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, constrainer.bounds.min.x, constrainer.bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, constrainer.bounds.min.y, constrainer.bounds.max.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, constrainer.bounds.min.z, constrainer.bounds.max.z);

        transform.position = clampedPosition;
    }
}
