using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using static UnityEngine.InputSystem.InputAction;

public class SwitchCanvasOnPause : MonoBehaviour
{
    [Required]
    public GameObject gamePlayCanvas;
    [Required]
    public GameObject pauseCanvas;

    [Required]
    public Camera pauseCam;

    private Camera gameCam;

    bool pauseActive = false;

    void Awake()
    {
        gameCam = Camera.main;
    }


    public void PauseGameEvent(CallbackContext cb)
    {
        Debug.Log("Switching Canvas");
        pauseActive = !pauseActive;

        Time.timeScale = (Time.timeScale == 1) ? 0 : 1;

        if (pauseActive)
        {
            gameCam.gameObject.SetActive(false);
            pauseCam.gameObject.SetActive(true);
            gamePlayCanvas.SetActive(false);
            pauseCanvas.SetActive(true);
        }
        else
        {
            pauseCam.gameObject.SetActive(false);
            gameCam.gameObject.SetActive(true);
            pauseCanvas.SetActive(false);
            gamePlayCanvas.SetActive(true);
        }
    }
}
