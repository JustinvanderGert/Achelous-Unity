//    using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BackButtonScriptPM : MonoBehaviour
//{
//    //public returnSystem
//    //nicolo was here

//    public GameObject PauseMenuHUD;
//    public GameObject InGameHUD;
//    public Camera AimCamera;

//    void Update()
//    {
//        if (returnSystem)
//        {
//            gameIsPaused = !gameIsPaused;
//            PauseGame();

//        }
//    }
//    void PauseGame()
//    {
//        if (gameIsPaused)
//        {
//            Time.timeScale = 0f;
//            PauseMenuHUD.SetActive(true);
//            Cursor.lockState = CursorLockMode.None;
//            Cursor.visible = true;
//            AimCamera.gameObject.SetActive(false);
//            InGameHUD.SetActive(false);
//        }
//        else
//        {
//            Time.timeScale = 1;
//            PauseMenuHUD.SetActive(false);
//            Cursor.lockState = CursorLockMode.Locked;
//            Cursor.visible = false;
//            AimCamera.gameObject.SetActive(true);
//            InGameHUD.SetActive(true);
//        }
//    }
//}
   