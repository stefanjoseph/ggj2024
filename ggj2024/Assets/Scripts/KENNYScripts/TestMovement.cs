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
            _isSmacking = true;
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

        HandSmack();
    }

    private void LateUpdate()
    {
        ConstrainPlayer();
    }

    private void HandSmack()
    {
        Vector3 pos = transform.position;

        //Commences Hand Slap
        if (_isSmacking == true)
        {
            transform.Translate(Vector3.right * _smackSpeed * Time.deltaTime);
        }

        //Resets Player position when smack ends
        if (transform.position.x >= constrainer.bounds.max.x && _isSmacking == true)
        {
            pos.x = constrainer.bounds.min.x;
            transform.position = pos;
        }

        //Starts the slap cooldown rate and enables the player to freely move again
        if (pos.x <= constrainer.bounds.min.x && _isSmacking == true)
        {
            _canSlap = Time.time + _slapRate;

            _isSmacking = false;
            _playerCanMove = true;
        }
    }

    //Box collider that contrains the player movement within the box
    private void ConstrainPlayer()
    {
        Vector3 clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, constrainer.bounds.min.x, constrainer.bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, constrainer.bounds.min.y, constrainer.bounds.max.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, constrainer.bounds.min.z, constrainer.bounds.max.z);

        transform.position = clampedPosition;
    }

}
    

