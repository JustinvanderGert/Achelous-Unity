using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonScriptPM : MonoBehaviour
{

    public GameObject PauseMenuHUD;

    public static bool gameIsPaused;
    void Start()
    {

        {
            gameIsPaused = !gameIsPaused;
            PauseGame();

        }
    }
    void PauseGame()
    {
        Time.timeScale = 1;
        PauseMenuHUD.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    
}
