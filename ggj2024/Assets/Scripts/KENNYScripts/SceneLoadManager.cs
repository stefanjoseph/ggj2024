using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
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
}
