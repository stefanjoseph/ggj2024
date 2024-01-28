using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour
{
    private PlayerInputActions _input;

    [SerializeField] private Image _grabIcon;

    [SerializeField] private float _speed = 5f;

    private float _canSlap = -1f;

    public BoxCollider constrainer;

    public GrabWindow grabWindow;

    public GameObject currentHeldObject;

    private void Start()
    {
        _input = new PlayerInputActions();
        _input.Player_V2.Enable();
        _input.Player_V2.Grab_Drop.performed += GrabOrDropPerformed;

        _playerCanMove = true;
    }

    private void GrabOrDropPerformed(InputAction.CallbackContext context)
    {
        if (Time.time > _canSlap && currentHeldObject == null && )
        {
            _isSlapping = true;
            _playerCanMove = false;
        }
    }

    private void Update()
    {
        var move = _input.Player_V2.Movement.ReadValue<Vector3>();

        transform.Translate(move * _speed * Time.deltaTime);

        if (Time.time < _canSlap)
        {
            var tempAlpha = _grabIcon.color;
            tempAlpha.a = 0.25f;
            _grabIcon.color = tempAlpha;
        }
        else
        {
            var tempAlpha = _grabIcon.color;
            tempAlpha.a = 1f;
            _grabIcon.color = tempAlpha;
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
