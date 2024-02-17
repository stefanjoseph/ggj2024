using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField] private Image _blackOverlay;
    [SerializeField] private AudioSource clickSound;

    private bool _fadeIsActive;
    private bool _loadPlayGame;


    private void Update()
    {
        if ( _fadeIsActive == true) 
        {
            var tempAlpha = _blackOverlay.color;
            tempAlpha.a += 0.5f * Time.deltaTime;
            _blackOverlay.color = tempAlpha;

            if (tempAlpha.a >= 1f)
            {
                _loadPlayGame = true;
                PlayGame();
            }
        }
        
    }
    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        _fadeIsActive = true;
        
        if (_loadPlayGame == true )
        {
            SceneManager.LoadScene("ALPHA");
        }
        else
        {
            clickSound.Play();
        }
        
    }

    public void PlayerSelect()
    {
        GameObject playerSelectResults = GameObject.FindGameObjectWithTag("PlayerSelectResults");
        if (playerSelectResults != null)
        {
            Destroy(playerSelectResults);
        }
        
        Time.timeScale = 1f;
        clickSound.Play();
        SceneManager.LoadScene("PlayerSelect");
    }

    public void TitleScene()
    {
        Destroy(GameObject.FindGameObjectWithTag("PlayerSelectResults"));
        clickSound.Play();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitlePage");
    }

    public void CreditsPage()
    {
        clickSound.Play();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("CreditsPage");
    }

    public void QuitGame()
    {
        clickSound.Play();
        Application.Quit();
    }

    public void Attributes()
    {
        clickSound.Play();
        Application.OpenURL("https://docs.google.com/document/d/1cmt5oxMAYGXtJimIhErYiBW2-qXPrWKOS0swgro2Apc/edit?usp=sharing");
    }

}
