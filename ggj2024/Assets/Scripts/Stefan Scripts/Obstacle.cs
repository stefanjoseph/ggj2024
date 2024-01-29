using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector2 relativePosition = new();

    public bool isVisible = false;

    public bool isMarkedForRemoval = false;

    public Obstacle(float x, float y)
    {
        relativePosition.x = x;
        relativePosition.y = y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
