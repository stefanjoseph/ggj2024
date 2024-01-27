using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleTarget : MonoBehaviour
{
    public float swerve_dist = 15.0f;
    public float swerve_speed = 2.0f;
    public float wander_target;
    private Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        wander_target = origin.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = origin + Vector3.right * Mathf.Sin(Time.time * swerve_speed) * swerve_dist;
    }
}
