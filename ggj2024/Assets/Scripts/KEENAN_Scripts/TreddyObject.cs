using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreddyObject : MonoBehaviour
{
    public Transform myWiggleTarget;
    private WiggleTarget wiggleTarget;
    private Rigidbody mybody; 
    public Vector3 look_dir;
    public float force_amount = 50.0f;
    public float torque = 10.0f;
    public float base_drag = 1.0f;
    public float settle_drag = 8.0f;
    private bool needs_boost = false;
    private bool needs_drag = false;
    public bool needs_death = false;

    // Start is called before the first frame update
    void Start()
    {
        mybody = GetComponent<Rigidbody>();
        mybody.drag = base_drag;
        wiggleTarget = myWiggleTarget.GetComponent<WiggleTarget>();
    }

    public void ApplyBoost() {
        needs_boost = true;
    }
    public void ApplyDrag() {
        needs_drag = true;
    }
    public void SettleDown() {
        mybody.drag = settle_drag;
        Invoke("ResetDrag", 3f);
    }
    public void ResetDrag() {
        mybody.drag = base_drag;
    }
    public void Die(Collision collision) {
        // Clear constraints
        mybody.constraints = RigidbodyConstraints.None;
        mybody.useGravity = true;
        mybody.AddForceAtPosition(Vector3.up * 10.0f + Vector3.forward * -1.0f, collision.transform.position, ForceMode.Impulse);
        needs_death = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 point_dir = myWiggleTarget.position - transform.position;
        // Rotation BS
        look_dir = point_dir - transform.forward;
        
        mybody.AddForce(Vector3.right * point_dir.x * force_amount);
        mybody.AddTorque(transform.up * torque * look_dir.x, ForceMode.Force);
        Debug.DrawRay(transform.position, look_dir, Color.green);

        if( needs_boost ) {
            mybody.AddForce(Vector3.forward * 0.5f);
        }

        if( needs_drag ) {
            mybody.AddForce(Vector3.forward * -0.5f);
        }

        if ( needs_death ) {
            mybody.AddForce(Vector3.forward * -50f);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.magnitude > 16 && !needs_death) {
            Die(collision);
        } else {
            Debug.Log("Non lethal collision");
            wiggleTarget.wander_target += Random.Range(-3.0f, 3.0f);
        }
        if (needs_death) {
            mybody.AddForce(Vector3.forward * -10f, ForceMode.Impulse);
        }
    }
}
