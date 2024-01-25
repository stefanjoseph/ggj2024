using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public GameObject obstacle;
    public List<GameObject> fallenObstacles = new();
    public float visibilityDistance;
    public float exitEdgePosition = 0.0f;
    public float unitsCoveredPerLoop;
    public float nearPlaneLocation;
    public float farPlaneLocation;
    public GameObject exitVantagePoint;
    public GameObject frontVantagePoint;
    public Vector2 unitScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.exitEdgePosition += unitsCoveredPerLoop;
        while (exitEdgePosition > 1.0f)
        {
            this.exitEdgePosition -= 1.0f;
        }

        this.nearPlaneLocation = ComputeTrackPositionWithWrapAround(exitEdgePosition);
        this.farPlaneLocation = ComputeTrackPositionWithWrapAround(exitEdgePosition + visibilityDistance);

        foreach (GameObject thisObstacle in fallenObstacles)
        {
            Obstacle obstacleComponent = thisObstacle.GetComponent<Obstacle>();
            if (IsObstacleWithinVisibleWindow(obstacleComponent))
            {
                thisObstacle.gameObject.SetActive(true);
                Vector3 newPosition = DeterminePositionUsingVantagePoint(ConvertToNearPlaneOffset(obstacleComponent.relativePosition));
                thisObstacle.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            }
            else
            {
                thisObstacle.gameObject.SetActive(false);
            }
        }

        ManuallyCreateObstacle();
    }

    public bool IsObstacleWithinVisibleWindow(Obstacle obstacle)
    {
        return obstacle.relativePosition.x >= this.nearPlaneLocation && obstacle.relativePosition.x < this.farPlaneLocation;
    }

    public float ComputeTrackPositionWithWrapAround(float position)
    {
        return position % 1.0f;
    }

    public Vector2 ConvertToNearPlaneOffset(float trackPositionX, float trackPositionY)
    {
        float nearPlaneOffsetX = trackPositionX - this.exitEdgePosition;
        float nearPlaneOffsetY = trackPositionY - 0.5f;

        Debug.Log($"nearPlaneOffsetX:{nearPlaneOffsetX} nearPlaneOffsetY:{nearPlaneOffsetY}");
        return new Vector2(nearPlaneOffsetX, nearPlaneOffsetY);
    }

    public Vector2 ConvertToNearPlaneOffset(Vector2 trackPosition)
    {
        return ConvertToNearPlaneOffset(trackPosition.x, trackPosition.y);
    }

    public Vector3 DeterminePositionUsingVantagePoint(Vector2 nearPlaneOffset)
    {
        // From the perspective of the exit vantage point, we are facing the positive z direction
        // Negative x is to our left and positive x is to our right
        float z = this.exitVantagePoint.transform.position.z + unitScale.x*nearPlaneOffset.x;
        float x = this.exitVantagePoint.transform.position.x + unitScale.y*nearPlaneOffset.y;

        Debug.Log($"finalX:{z} finalY:{x}");
        return new Vector3(x, 0.0f, z);
    }

    public void CreateObstacle(float x, float y)
    {
        GameObject newObstacle = Instantiate(obstacle, DeterminePositionUsingVantagePoint(ConvertToNearPlaneOffset(x, y)), Quaternion.identity);

        newObstacle.GetComponent<Obstacle>().relativePosition = new(x, y);

        this.fallenObstacles.Add(newObstacle);
    }

    public void ManuallyCreateObstacle()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CreateObstacle(this.exitEdgePosition, 0.5f);
        }
    }
}
