using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject fallingObstacle;
    public GameObject obstacleRamp;
    public GameObject obstacleCube;
    public GameObject obstacleArch;
    public List<GameObject> fallingObstacles = new();
    public List<GameObject> fallenObstacles = new();
    public float visibilityDistance;
    public float exitEdgePosition = 0.0f;
    public float unitsCoveredPerLoop;
    public float nearPlaneLocation;
    public float farPlaneLocation;
    public GameObject exitVantagePoint;
    public GameObject frontVantagePoint;
    public Vector2 unitScale;
    public bool shouldDropObjects;
    public bool shouldUseRealObstacles;
    public float SPAWN_HEIGHT;
    public float TREAD_HEIGHT;

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
                Vector3 newPosition = DetermineAbsolutePositionUsingVantagePoint(ConvertToExitEdgeOffset(obstacleComponent.relativePosition));
                thisObstacle.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            }
            else
            {
                thisObstacle.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < fallingObstacles.Count; i++)
        {
            GameObject thisObstacle = fallingObstacles[i];
            CheckForObstacleFreezes(thisObstacle);
        }

        if (shouldDropObjects)
        {
            ManuallyDropObstacle();
        }
        else
        {
            ManuallyCreateStaticObstacle();
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
        float nearPlaneOffsetX = trackPositionX - this.exitEdgePosition;

        nearPlaneOffsetX = nearPlaneOffsetX < 0 ? 1.0f - this.exitEdgePosition + trackPositionX : nearPlaneOffsetX;

        float nearPlaneOffsetY = trackPositionY - 0.5f;

        // Debug.Log($"nearPlaneOffsetX:{nearPlaneOffsetX} nearPlaneOffsetY:{nearPlaneOffsetY}");
        return new Vector2(nearPlaneOffsetX, nearPlaneOffsetY);
    }

    public Vector2 ConvertToExitEdgeOffset(Vector2 trackPosition)
    {
        return ConvertToExitEdgeOffset(trackPosition.x, trackPosition.y);
    }

    public Vector3 DetermineAbsolutePositionUsingVantagePoint(Vector2 exitEdgeOffset)
    {
        // From the perspective of the exit vantage point, we are facing the positive z direction
        // Negative x is to our left and positive x is to our right
        float z = this.exitVantagePoint.transform.position.z + unitScale.x*exitEdgeOffset.x;
        float x = this.exitVantagePoint.transform.position.x + unitScale.y*exitEdgeOffset.y;

        // Debug.Log($"finalX:{z} finalY:{x}");
        return new Vector3(x, 0.0f, z);
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

        obstacle.GetComponent<Obstacle>().relativePosition = ConvertToRelativePosition(DetermineExitEdgeOffsetUsingVantagePoint(obstacle.transform.position));

        this.fallenObstacles.Add(obstacle);
    }

    public void CheckForObstacleFreezes(GameObject obstacle)
    {
        if (obstacle.transform.position.y <= TREAD_HEIGHT)
        {
            fallingObstacles.Remove(obstacle);
            ConvertToStaticObstacle(obstacle);
        }
    }

    public void CreateStaticObstacle(float x, float y)
    {
        GameObject newObstacle = Instantiate(SelectObstacle(), DetermineAbsolutePositionUsingVantagePoint(ConvertToExitEdgeOffset(x, y)), Quaternion.identity);

        newObstacle.GetComponent<Obstacle>().relativePosition = new(x, y);

        this.fallenObstacles.Add(newObstacle);
    }

    public void ManuallyCreateStaticObstacle()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CreateStaticObstacle(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }

    public void ManuallyDropObstacle()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            float x = Random.Range(0.0f, visibilityDistance);
            float y = Random.Range(0.0f, 1.0f);

            Vector3 spawnPointWithoutHeight = DetermineAbsolutePositionUsingVantagePoint(ConvertToExitEdgeOffset(x, y));

            GameObject droppedObstacle = Instantiate(SelectObstacle(), new Vector3(spawnPointWithoutHeight.x, SPAWN_HEIGHT, spawnPointWithoutHeight.z), Random.rotation);
            
            fallingObstacles.Add(droppedObstacle);
        }
    }

    public GameObject SelectObstacle()
    {
        GameObject selectedObstacle;
        
        if (!shouldUseRealObstacles)
        {
            selectedObstacle = obstacle;
        }
        else
        {
            selectedObstacle = Random.Range(0, 3) switch
            {
                0 => obstacleArch,
                1 => obstacleCube,
                _ => obstacleRamp,
            };
        }

        return selectedObstacle;
    }

}
