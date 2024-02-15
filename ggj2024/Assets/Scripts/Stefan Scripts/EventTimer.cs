using TMPro;
using UnityEngine;

public class EventTimer : MonoBehaviour
{
    public float NORMAL_TIMER_INTERVAL_IN_SECONDS;

    public MagicBeanController magicBeans;

    public TrackSpeedMultiplier trackSpeedMultiplier;

    [SerializeField] private float _timeRemaining;

    private bool _timerIsRunning;

    public TMP_Text timeText;

    public bool shouldIncrementTime;

    public int ODDS_FAST;
    public float FAST_TIMER_INTERVAL_IN_SECONDS;
    public float FAST_MULTIPLIER;
    public int ODDS_SLOW;
    public float SLOW_TIMER_INTERVAL_IN_SECONDS;
    public float SLOW_MULTIPLIER;

    public AudioSource laughTrack;

    private void Start()
    {
        _timerIsRunning = true;
        _timeRemaining = NORMAL_TIMER_INTERVAL_IN_SECONDS;
    }

    private void Update()
    {
        if (!shouldIncrementTime)
        {
            return;
        }

        if (!magicBeans.shouldBeFloating && trackSpeedMultiplier.value == 1.0f)
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
                Debug.Log("Timer has expired... Choosing event.");
                _timeRemaining = 0;
                _timerIsRunning = false;

                SelectSpecialEvent();
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
        _timeRemaining = NORMAL_TIMER_INTERVAL_IN_SECONDS;
        DisplayTime(_timeRemaining);
    }

    private void SelectSpecialEvent()
    {
        int selectionSeed = Random.Range(0, 100);

        if (selectionSeed < ODDS_FAST)
        {
            trackSpeedMultiplier.ChangeSpeedForInterval(FAST_MULTIPLIER, FAST_TIMER_INTERVAL_IN_SECONDS);
            timeText.text = "S P E E D  U P";
        }
        else if (selectionSeed < ODDS_FAST + ODDS_SLOW)
        {
            trackSpeedMultiplier.ChangeSpeedForInterval(SLOW_MULTIPLIER, SLOW_TIMER_INTERVAL_IN_SECONDS);
            timeText.text = "S L O W  D O W N";
        }
        else
        {
            magicBeans.shouldBeFloating = true;
            timeText.text = "M A G I C  B E A N S";
        }

        laughTrack.Play();
    }

}
