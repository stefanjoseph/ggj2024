using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreddyObject : MonoBehaviour
{
    public Transform myWiggleTarget;
    private Rigidbody mybody; 
    public Vector3 look_dir;
    public float force_amount = 50.0f;
    public float torque = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        mybody = GetComponent<Rigidbody>();
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
    }
}
