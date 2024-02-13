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
    public float X_AXIS_OFFSET;
    public float OBJECT_DROP_RATE_IN_SECONDS;
    public float timeSinceLastDrop = 0f;
    public bool shouldUseEqualProbability;
    public int ODDS_RAMP;
    public int ODDS_ARCH;
    public bool shouldIncrementTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (Time.timeScale == 0 || !shouldIncrementTime)
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
        float x = Random.Range(-X_AXIS_OFFSET, X_AXIS_OFFSET);

        GameObject droppedObstacle = Instantiate(SelectObstacle(), new Vector3(x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(Vector3.up * 180));
        
        fallingObstacles.Add(droppedObstacle);
    }

    public GameObject SelectObstacle()
    {
        GameObject selectedObstacle;
        
        if (shouldUseEqualProbability)
        {
            selectedObstacle = Random.Range(0, 3) switch
            {
                0 => obstacleArch,
                1 => obstacleCube,
                _ => obstacleRamp,
            };
        }
        else
        {
            int selectionSeed = Random.Range(0, 100);
            if (selectionSeed < ODDS_ARCH)
            {
                selectedObstacle = obstacleArch;
            }
            else if (selectionSeed < ODDS_ARCH + ODDS_RAMP)
            {
                selectedObstacle = obstacleRamp;
            }
            else
            {
                selectedObstacle = obstacleCube;
            }
        }
    
        return selectedObstacle;
    }

}
