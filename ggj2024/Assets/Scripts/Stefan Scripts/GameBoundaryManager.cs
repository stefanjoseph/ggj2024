using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoundaryManager : MonoBehaviour
{
    public AudioSource gameplayMusic;

    [SerializeField] private GameObject _congratulations;
    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _player2;
   
    private void OnTriggerEnter(Collider other)
    {
        if (HasGrandparent(other.gameObject) && (other.transform.parent.transform.parent.tag == "Player1Object" || other.transform.parent.transform.parent.tag == "Player2Object"))
        {
            if (other != null)
            {
                Time.timeScale = 0f;
                gameplayMusic.Stop();
                _congratulations.SetActive(true);
                if (other.transform.parent.transform.parent.tag == "Player1Object" && !_player1.activeSelf)
                {
                    Debug.Log("Player 2 Won!");
                    _player2.SetActive(true);
                }
                else if (!_player2.activeSelf)
                {
                    Debug.Log("Player 1 Won!");
                    _player1.SetActive(true);
                }
            }
        } else if (other.tag == "ObjectDrop" || other.tag == "ObjectTrack")
        {
            Debug.Log("Destroying Fallen Object");
            Destroy(other.gameObject);
        }
    }

    private bool HasGrandparent(GameObject obj)
    {
        return obj != null && obj.transform.parent != null && obj.transform.parent.transform.parent != null;
    }
}
