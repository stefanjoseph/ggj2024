using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectManager : MonoBehaviour
{
    public TrackManager trackManager;
    public GameObject obstacleRamp;
    public GameObject obstacleCube;
    public GameObject obstacleArch;
    public List<GameObject> fallingObstacles = new();
    public float SPAWN_HEIGHT;
    public float OBJECT_DROP_RATE_IN_SECONDS;
    public float timeSinceLastDrop = 0f;

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
        timeSinceLastDrop += Time.deltaTime;

        if (timeSinceLastDrop >= OBJECT_DROP_RATE_IN_SECONDS)
        {
            ManuallyDropObstacle();
            timeSinceLastDrop -= OBJECT_DROP_RATE_IN_SECONDS;
        }
    }

    public void ManuallyDropObstacle()
    {
        float x = Random.Range(0.2f, trackManager.visibilityDistance - 0.1f);
        float y = Random.Range(0.2f, 0.8f);

        Vector3 spawnPointWithoutHeight = trackManager.DetermineAbsolutePositionUsingVantagePoint(trackManager.ConvertToExitEdgeOffset(x, y), 0);

        GameObject droppedObstacle = Instantiate(SelectObstacle(), new Vector3(spawnPointWithoutHeight.x, SPAWN_HEIGHT, spawnPointWithoutHeight.z), Quaternion.identity);
        
        fallingObstacles.Add(droppedObstacle);
    }

    public GameObject SelectObstacle()
    {
        GameObject selectedObstacle;
        
        selectedObstacle = Random.Range(0, 3) switch
        {
            0 => obstacleArch,
            1 => obstacleCube,
            _ => obstacleRamp,
        };
    
        return selectedObstacle;
    }

}
