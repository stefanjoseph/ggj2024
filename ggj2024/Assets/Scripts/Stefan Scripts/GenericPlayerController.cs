using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GenericPlayerController : MonoBehaviour
{
    private PlayerInputActions _input;

    [SerializeField] private Image _grabIcon;

    [SerializeField] private float _speed = 5f;

    private float _nextGrabTime = -1f;

    public float GRAB_COOLDOWN_IN_SECONDS = 6f;

    public BoxCollider constrainer;

    public GrabWindow grabWindow;

    public GameObject currentHeldObject;

    protected void GrabOrDropPerformed(InputAction.CallbackContext context)
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
        Debug.Log("Initiating grab");
        currentHeldObject = grabWindow.grabbableObject;

        Obstacle obstacleComponent = currentHeldObject.GetComponent<Obstacle>();

        obstacleComponent.isOnTrack = false;
        obstacleComponent.isMarkedForRemoval = true;

        grabWindow.grabbableObject = null;

        currentHeldObject.transform.SetParent(grabWindow.transform);

        currentHeldObject.GetComponent<Rigidbody>().isKinematic = true;

        currentHeldObject.transform.position = this.transform.position - new Vector3(0, obstacleComponent.GRAB_OFFSET, 0);
    }

    private void DropObject()
    {
        Debug.Log("Initiating drop");
        currentHeldObject.transform.parent = null;
        currentHeldObject.GetComponent<Obstacle>().isMarkedForRemoval = false;
        currentHeldObject.GetComponent<Rigidbody>().useGravity = true;
        currentHeldObject.GetComponent<Rigidbody>().isKinematic = false;

        currentHeldObject = null;

        // Starts the grab cooldown rate
        _nextGrabTime = Time.time + GRAB_COOLDOWN_IN_SECONDS;
    }

    protected void GenericUpdate(Vector3 move)
    {
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

        if (currentHeldObject != null)
        {
            currentHeldObject.transform.position = this.transform.position - new Vector3(0, currentHeldObject.GetComponent<Obstacle>().GRAB_OFFSET, 0);
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
