using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreddyObject : MonoBehaviour
{
    public float swerve_degs = 15.0f;
    public float swerve_speed = 2.0f;
    public Vector3 point_dir;
    private float theta = 0f;
    private Rigidbody mybody; 
    private Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        mybody = GetComponent<Rigidbody>();
        origin = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotation BS
        // float singleStep = swerve_speed * Time.deltaTime;
        // point_dir = new Vector3(Mathf.Sin(Time.time * swerve_speed), 0f, Mathf.Cos(Time.time * swerve_speed));
        // Vector3 newDirection = Vector3.RotateTowards(transform.forward, point_dir, singleStep, 0.0f);
        // Debug.DrawRay(transform.position, newDirection, Color.red);
        // transform.eulerAngles = point_dir;

        // Translation
        // mybody.AddForce(transform.right * Mathf.Sin(Time.time * swerve_speed) * swerve_degs);
        mybody.MovePosition(origin + Vector3.right * Mathf.Sin(Time.time * swerve_speed) * swerve_degs);
    }
}
