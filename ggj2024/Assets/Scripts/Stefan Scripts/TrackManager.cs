using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public Camera theCamera;
    public GameObject obstacle;
    public List<Obstacle> fallenObstacles = new();
    public float visibilityDistance;
    public float exitEdgePosition = 0.0f;
    public float unitsCoveredPerLoop;
    public float nearPlaneLocation;
    public float farPlaneLocation;

    public Vector2 unitScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        exitEdgePosition += unitsCoveredPerLoop;
        while (exitEdgePosition > 1.0f)
        {
            exitEdgePosition -= 1.0f;
        }

        this.nearPlaneLocation = ComputeTrackPositionWithWrapAround(exitEdgePosition);
        this.farPlaneLocation = ComputeTrackPositionWithWrapAround(exitEdgePosition + visibilityDistance);

        foreach (Obstacle thisObstacle in fallenObstacles)
        {
            if (IsObstacleWithinVisibileWindow(thisObstacle))
            {
                thisObstacle.isVisible = true;
                thisObstacle.transform.position = DeterminePositionUsingCamera(ConvertToNearPlaneOffset(thisObstacle.relativePosition));
            }
            else
            {
                thisObstacle.isVisible = false;
            }
        }

        ManuallyCreateObstacle();
    }

    public bool IsObstacleWithinVisibileWindow(Obstacle obstacle)
    {
        return obstacle.relativePosition.x >= this.nearPlaneLocation && obstacle.relativePosition.x < this.farPlaneLocation;
    }

    public float ComputeTrackPositionWithWrapAround(float position)
    {
        return position % 1.0f;
    }

    public Vector2 ConvertToNearPlaneOffset(float trackPositionX, float trackPositionY)
    {
        return new Vector2(trackPositionX - exitEdgePosition, trackPositionY - 0.5f);
    }

    public Vector2 ConvertToNearPlaneOffset(Vector2 trackPosition)
    {
        return ConvertToNearPlaneOffset(trackPosition.x, trackPosition.y);
    }

    public Vector2 DeterminePositionUsingCamera(Vector2 nearPlaneOffset)
    {
        return new Vector3(unitScale.x*(theCamera.transform.position.y + nearPlaneOffset.y), 0.0f, unitScale.y*(theCamera.transform.position.x + nearPlaneOffset.x));
    }

    public void CreateObstacle(float x, float y)
    {
        GameObject newObstacle = Instantiate(obstacle, DeterminePositionUsingCamera(ConvertToNearPlaneOffset(x, y)), Quaternion.identity);

        newObstacle.GetComponent<Obstacle>().relativePosition = new Vector2(x, y);

        fallenObstacles.Add(newObstacle.GetComponent<Obstacle>());
    }

    public void ManuallyCreateObstacle()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CreateObstacle(exitEdgePosition, 0.5f);
        }
    }
}
