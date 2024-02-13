using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBeanController : MonoBehaviour
{
    public Vector3 startPos;

    public Vector3 endPos;

    public float TOTAL_FLOAT_TIME;

    public float elapsedFloatTime;

    public bool shouldBeFloating;
    // Start is called before the first frame update
    void Start()
    {
        elapsedFloatTime = 0f;
    }

    void Awake()
    {
        elapsedFloatTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldBeFloating)
        {
            elapsedFloatTime = 0f;
            this.gameObject.transform.position = startPos;
            return;
        }

        elapsedFloatTime += Time.deltaTime;

        if (elapsedFloatTime >= TOTAL_FLOAT_TIME)
        {
            shouldBeFloating = false;
        }

        this.gameObject.transform.position = Vector3.Lerp(startPos, endPos, elapsedFloatTime/TOTAL_FLOAT_TIME);
    }

    void OnTriggerStay(Collider other) {
        if (other != null)
        {
            Obstacle potentialComponent = other.GetComponent<Obstacle>();
    
            if (potentialComponent != null && potentialComponent.isOnTrack)
            {
                potentialComponent.isMarkedForPermanentRemoval = true;
                potentialComponent.isMarkedForRemoval = true;
            }
        }
    }
}