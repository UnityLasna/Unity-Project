using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScensScript : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseUI;
    public GameObject optionsUI;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused) {
                Resume();
            } else {
                Pause();
            }
        }
        
    }

    public void Resume() {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;

    }

    void Pause() {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Menu() {
       Time.timeScale = 1f;
       SceneManager.LoadScene("Manus");
       
    }

    public void Quit() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void NewGame() {
        SceneManager.LoadScene("Level 1");
        Debug.Log("Loading level 1");
    }

    public void Options ()
    {
        optionsUI.SetActive(true);
        Debug.Log("ladataan optionsit");

    }

    public void Back ()
    {
        optionsUI.SetActive(false);
    }
}
