using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public List<GameObject> fallenObstacles = new();
    public float visibilityDistance;
    public float exitEdgePosition = 0.0f;
    public float UNITS_COVERED_PER_SECOND;
    public float nearPlaneLocation;
    public float farPlaneLocation;
    public GameObject exitVantagePoint;
    public GameObject frontVantagePoint;
    public Vector2 unitScale;
    public float nearPlaneOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        this.exitEdgePosition += UNITS_COVERED_PER_SECOND*Time.deltaTime;
        while (exitEdgePosition > 1.0f)
        {
            this.exitEdgePosition -= 1.0f;
        }

        this.nearPlaneLocation = ComputeTrackPositionWithWrapAround(exitEdgePosition + nearPlaneOffset);
        this.farPlaneLocation = ComputeTrackPositionWithWrapAround(exitEdgePosition + visibilityDistance);

        TakeRemovedObstaclesOffTrack();

        foreach (GameObject thisObstacle in fallenObstacles)
        {
            Obstacle obstacleComponent = thisObstacle.GetComponent<Obstacle>();
            if (IsObstacleWithinVisibleWindow(obstacleComponent))
            {
                thisObstacle.gameObject.SetActive(true);
                Vector3 newPosition = DetermineAbsolutePositionUsingVantagePoint(ConvertToExitEdgeOffset(obstacleComponent.relativePosition), thisObstacle.gameObject.transform.position.y);
                thisObstacle.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            }
            else
            {
                thisObstacle.gameObject.SetActive(false);
            }
        }
    }

    public bool IsObstacleWithinVisibleWindow(Obstacle obstacle)
    {
        if (this.farPlaneLocation > this.nearPlaneLocation)
        {
            return obstacle.relativePosition.x >= this.nearPlaneLocation && obstacle.relativePosition.x < this.farPlaneLocation;
        }
        else
        {
            return obstacle.relativePosition.x >= this.nearPlaneLocation && obstacle.relativePosition.x <= 1.0f
                || obstacle.relativePosition.x >= 0.0f && obstacle.relativePosition.x < this.farPlaneLocation;
        }
        
    }

    public float ComputeTrackPositionWithWrapAround(float position)
    {
        return position % 1.0f;
    }

    public Vector2 ConvertToExitEdgeOffset(float trackPositionX, float trackPositionY)
    {
        float exitEdgeOffsetX = trackPositionX - this.exitEdgePosition;

        exitEdgeOffsetX = exitEdgeOffsetX < 0 ? 1.0f - this.exitEdgePosition + trackPositionX : exitEdgeOffsetX;

        float exitEdgeOffsetY = trackPositionY - 0.5f;

        return new Vector2(exitEdgeOffsetX, exitEdgeOffsetY);
    }

    public Vector2 ConvertToExitEdgeOffset(Vector2 trackPosition)
    {
        return ConvertToExitEdgeOffset(trackPosition.x, trackPosition.y);
    }

    public Vector3 DetermineAbsolutePositionUsingVantagePoint(Vector2 exitEdgeOffset, float height)
    {
        // From the perspective of the exit vantage point, we are facing the positive z direction
        // Negative x is to our left and positive x is to our right
        float z = this.exitVantagePoint.transform.position.z + unitScale.x*exitEdgeOffset.x;
        float x = this.exitVantagePoint.transform.position.x + unitScale.y*exitEdgeOffset.y;

        // Debug.Log($"finalX:{z} finalY:{x}");
        return new Vector3(x, height, z);
    }

    public Vector2 DetermineExitEdgeOffsetUsingVantagePoint(Vector3 absolutePosition)
    {
        Vector2 exitEdgeOffset = new()
        {
            // From the perspective of the exit vantage point, we are facing the positive z direction
            // Negative x is to our left and positive x is to our right
            x = (absolutePosition.z - this.exitVantagePoint.transform.position.z) / unitScale.x,
            y = (absolutePosition.x - this.exitVantagePoint.transform.position.x) / unitScale.y
        };

        return exitEdgeOffset;
    }

    public Vector2 ConvertToRelativePosition(Vector2 exitEdgeOffset)
    {
        float trackPositionX = exitEdgeOffset.x + this.exitEdgePosition;

        trackPositionX = trackPositionX > 1.0f ? trackPositionX - 1.0f : trackPositionX;

        float trackPositionY = exitEdgeOffset.y + 0.5f;

        return new Vector2(trackPositionX, trackPositionY);
    }

    public void ConvertToStaticObstacle(GameObject obstacle)
    {
        obstacle.GetComponent<Rigidbody>().useGravity = false;
        obstacle.GetComponent<Rigidbody>().isKinematic = true;

        Obstacle obstacleComponent = obstacle.GetComponent<Obstacle>();

        obstacle.gameObject.transform.position += new Vector3(0, obstacleComponent.TRACK_OFFSET, 0);

        obstacleComponent.relativePosition = ConvertToRelativePosition(DetermineExitEdgeOffsetUsingVantagePoint(obstacle.transform.position));

        this.fallenObstacles.Add(obstacle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.GetComponent<Obstacle>() == null) && !other.GetComponent<Obstacle>().isOnTrack)
        {
            //Add SFX Here?
            other.GetComponent<Obstacle>().isOnTrack = true;
            ConvertToStaticObstacle(other.gameObject);
        }
    }

    private bool IsObstacleMarkedForRemoval(GameObject obstacleObj)
    {
        Obstacle obstacle = obstacleObj.GetComponent<Obstacle>();
        return obstacle.isMarkedForRemoval;
    }

    public void TakeRemovedObstaclesOffTrack()
    {
        fallenObstacles.RemoveAll(IsObstacleMarkedForRemoval);
    }

}
