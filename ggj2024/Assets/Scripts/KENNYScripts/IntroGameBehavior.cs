using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroGameBehavior : MonoBehaviour
{
    public FallingObjectManager fallingObjectManager;
    public EventTimer eventTimer;
    [SerializeField] private GameObject _readyTitle;
    [SerializeField] private GameObject _startTitle;
    [SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _readyStartContainer;

    [SerializeField] private Image _blackOverlay;

    private bool _startGame;

    void Start()
    {
        //StartCoroutine(BeginGameRoutine());
    }

    private void Update()
    {
        FadeToBlack();
    }
    IEnumerator BeginGameRoutine()
    {

        _readyTitle.SetActive(true);

        yield return new WaitForSeconds(3f);
        _readyTitle.GetComponentInChildren<Image>().color = Color.clear;
        _readyTitle.SetActive(false);
        _startTitle.SetActive(true);

        yield return new WaitForSeconds(3f);
        _startTitle.SetActive(false);
        _hud.SetActive(true);
        _readyStartContainer.SetActive(false);

        //Set bool to start the game
        fallingObjectManager.shouldIncrementTime = true;
        eventTimer.shouldIncrementTime = true;
    }

    private void FadeToBlack()
    {
        var tempAlpha = _blackOverlay.color;
        //tempAlpha.a = 1f;
        tempAlpha.a -= 0.33f * Time.deltaTime;
        _blackOverlay.color = tempAlpha;

        if (tempAlpha.a <= 0f)
        {
            StartCoroutine(BeginGameRoutine());
        }
    }

    private void AssignIcons()
    {

    }
}
