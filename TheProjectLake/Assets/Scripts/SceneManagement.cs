using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CodeMonkey.Utils;
using UnityEngine.EventSystems;

public class SceneManagement : MonoBehaviour
{
    public GameObject PausePanel;
    

    public static bool GameIsPaused = false;

    public Texture2D cursorArrow;

    public void PlayGame()
    {
       
        SceneManager.LoadSceneAsync("Game");
        
       
    }


    
    

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("resume");
        
        

    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("pause");
    }

    private void Awake()
    {
        Time.timeScale = 1;

        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            
        }
    }

}
