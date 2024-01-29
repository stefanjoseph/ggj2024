using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour
{
    private PlayerInputActions _input;

    [SerializeField] private Image _grabIcon;

    [SerializeField] private float _speed = 5f;

    private float _nextGrabTime = -1f;

    public float GRAB_COOLDOWN_IN_SECONDS = 6f;

    public BoxCollider constrainer;

    public GrabWindow grabWindow;

    public GameObject currentHeldObject;

    private void Start()
    {
        _input = new PlayerInputActions();
        _input.Player2_V2.Enable();
        _input.Player2_V2.Grab_Drop.performed += GrabOrDropPerformed;
    }

    private void GrabOrDropPerformed(InputAction.CallbackContext context)
    {
        if (Time.time > _nextGrabTime && currentHeldObject == null && grabWindow.grabbableObject != null)
        {
            GrabObject(); 
        }
        else if (currentHeldObject != null)
        {
            DropObject();            
        }
    }

    private void GrabObject()
    {
        currentHeldObject = grabWindow.grabbableObject;
        currentHeldObject.tag = "ObjectDrop";
        grabWindow.grabbableObject = null;

        currentHeldObject.transform.parent = gameObject.transform;

        currentHeldObject.transform.position = Vector3.zero;
        
        currentHeldObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void DropObject()
    {
        currentHeldObject.transform.parent = null;
        currentHeldObject.GetComponent<Rigidbody>().useGravity = true;

        currentHeldObject = null;

        // Starts the grab cooldown rate
        _nextGrabTime = Time.time + GRAB_COOLDOWN_IN_SECONDS;
    }

    private void Update()
    {
        var move = _input.Player2_V2.Movement.ReadValue<Vector3>();

        transform.Translate(move * _speed * Time.deltaTime);

        if (Time.time < _nextGrabTime)
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
    }

    private void LateUpdate()
    {
        ConstrainPlayer();
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
