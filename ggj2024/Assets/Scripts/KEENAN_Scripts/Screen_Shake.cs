using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_Shake : MonoBehaviour
{
    private Vector3 origin;
    public Vector3 shakeAmount;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 random_shake = new Vector3(Random.Range(-shakeAmount.x, shakeAmount.x),
                                   Random.Range(-shakeAmount.y, shakeAmount.y),
                                   Random.Range(-shakeAmount.z, shakeAmount.z));
        transform.position = origin + random_shake;
    }
}
