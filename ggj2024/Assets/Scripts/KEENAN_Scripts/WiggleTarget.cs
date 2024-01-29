using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleTarget : MonoBehaviour
{
    public float swerve_dist = 15.0f;
    public float swerve_speed = 2.0f;
    public float wander_target;
    private Vector3 origin;
    private float z_offs;
    private float time_offs;
    private float swerve_speed_wiggle;
    private float swerve_dist_wiggle;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        z_offs = origin.z - transform.parent.position.z;
        wander_target = transform.position.x - transform.parent.position.x;
        time_offs = Random.Range(0.0f,10.0f);
        swerve_speed_wiggle = Random.Range(-0.5f,0.5f);
        swerve_speed_wiggle = Random.Range(-0.5f,0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.parent.position + (Vector3.right * wander_target) + Vector3.right * Mathf.Sin((Time.time + time_offs) * (swerve_speed + swerve_speed_wiggle)) * (swerve_dist + swerve_dist_wiggle) + Vector3.forward * z_offs;
    }
}
