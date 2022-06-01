using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPause : MonoBehaviour
{
    public GameObject PauseMenuHUD;

        public static bool gameIsPaused;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                gameIsPaused = !gameIsPaused;
                PauseGame();
            
            }
        }
        void PauseGame()
        {
            if (gameIsPaused)
            {
                Time.timeScale = 0f;
            PauseMenuHUD.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
            else
            {
                Time.timeScale = 1;
            PauseMenuHUD.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        }
    }