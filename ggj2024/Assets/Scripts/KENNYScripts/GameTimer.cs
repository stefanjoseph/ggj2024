using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float _timeRemaining = 3f;

    private bool _timerIsRunning;

    public TMP_Text timeText;

    private void Start()
    {
        _timerIsRunning = true;
    }

    private void Update()
    {
        RunningTimer();
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    private void RunningTimer()
    {
        if (_timerIsRunning == true)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                DisplayTime(_timeRemaining);
            }
            else
            {
                Debug.Log("Out of time sucka!");
                _timeRemaining = 0;
                _timerIsRunning = false;
            }
        }
    }
}
