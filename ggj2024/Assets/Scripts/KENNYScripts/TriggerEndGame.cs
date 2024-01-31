using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndGame : MonoBehaviour
{

    [SerializeField] private GameObject _congratulations;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1Object" ||  other.tag == "Player2Object")
        {
            if (other != null)
            {
                Time.timeScale = 0f;
                _congratulations.SetActive(true);
            }
        }
    }
}
