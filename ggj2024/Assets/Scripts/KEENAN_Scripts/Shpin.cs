using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shpin : MonoBehaviour
{

    public Vector3 rotation_rate = new Vector3(0,1,0);

    private TrackSpeedMultiplier trackSpeedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        trackSpeedMultiplier = GameObject.FindGameObjectWithTag("TrackSpeedMultiplier").GetComponent<TrackSpeedMultiplier>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(trackSpeedMultiplier.value * rotation_rate * Time.deltaTime, Space.Self);
    }
}
