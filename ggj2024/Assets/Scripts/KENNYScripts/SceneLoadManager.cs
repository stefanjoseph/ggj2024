using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("ALPHA");
    }

    public void TitleScene()
    {

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitlePage");
        
    }

    public void CreditsPage()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("CreditsPage");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Attributes()
    {
        Application.OpenURL("https://docs.google.com/document/d/1cmt5oxMAYGXtJimIhErYiBW2-qXPrWKOS0swgro2Apc/edit?usp=sharing");
    }

}
