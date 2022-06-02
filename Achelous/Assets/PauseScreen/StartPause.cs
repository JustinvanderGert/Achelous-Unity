using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartPause : MonoBehaviour
{
    public GameObject PauseMenuHUD;
    public GameObject InGameHUD;
    public Camera AimCamera;

    public static bool gameIsPaused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();

        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OnClick()
    {
        gameIsPaused = !gameIsPaused;
        PauseGame();

    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            PauseMenuHUD.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            AimCamera.gameObject.SetActive(false);
            InGameHUD.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            PauseMenuHUD.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            AimCamera.gameObject.SetActive(true);
            InGameHUD.SetActive(true);
        }
    }
}