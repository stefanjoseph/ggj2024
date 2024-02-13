using TMPro;
using UnityEngine;

public class EventTimer : MonoBehaviour
{
    public float FULL_TIMER_INTERVAL_IN_SECONDS;

    public MagicBeanController magicBeans;

    [SerializeField] private float _timeRemaining;

    private bool _timerIsRunning;

    public TMP_Text timeText;

    public bool shouldIncrementTime;

    private void Start()
    {
        _timerIsRunning = true;
        _timeRemaining = FULL_TIMER_INTERVAL_IN_SECONDS;
    }

    private void Update()
    {
        if (!shouldIncrementTime)
        {
            return;
        }

        if (!magicBeans.shouldBeFloating)
        {
            RunTimer();
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    private void RunTimer()
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
                Debug.Log("Timer has expired... Release the Beans.");
                _timeRemaining = 0;
                _timerIsRunning = false;

                magicBeans.shouldBeFloating = true;
                timeText.text = "M A G I C  B E A N S";
            }
        }
        else
        {
            ResetTimer();
        }
    }
    
    public void ResetTimer()
    {
        _timerIsRunning = true;
        _timeRemaining = FULL_TIMER_INTERVAL_IN_SECONDS;
        DisplayTime(_timeRemaining);
    }
}
