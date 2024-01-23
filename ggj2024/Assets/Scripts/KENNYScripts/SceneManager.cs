using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void PlayGame()
    //{
    //    //Temporary for the title screen to load the game

    //    Time.timeScale = 1.0f;
    //    //SceneManager.LoadScene("Game");
    //}

    //public void TitleScene()
    //{
    //    //Temporary to go back to the title screen

    //    Time.timeScale = 1.0f;
    //    SceneManager.LoadScene("TitleScreen");
    //}

    public void QuitGame()
    {
        Application.Quit();
    }
}
