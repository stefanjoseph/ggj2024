using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpeedMultiplier : MonoBehaviour
{
    public float value = 1.0f;

    public float remainingTime = 0f;

    private void Update()
    {
        if (remainingTime == 0)
        {
            return;
        }

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            value = 1.0f;
            remainingTime = 0f;
        }
    }

    public void ChangeSpeedForInterval(float newSpeed, float time)
    {
        value = newSpeed;
        remainingTime = time;
    }
}
