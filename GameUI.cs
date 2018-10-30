using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject resumeButton;
    [SerializeField]
    private GameObject pauseText;
    [SerializeField]
    private GameObject quitButton;

    public void TryAgainClick()
    {
        SceneManager.LoadScene(1);
    }

    public void ResumeClick()
    {
        Time.timeScale = 1.0f;
        EventManager.TriggerEvent("EnableControls");
        resumeButton.SetActive(false);
        pauseText.SetActive(false);
        quitButton.SetActive(false);


    }

    public void QuitClick()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.0f;
            EventManager.TriggerEvent("DisableControls");
            resumeButton.SetActive(true);
            pauseText.SetActive(true);
            quitButton.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
            EventManager.TriggerEvent("EnableControls");
            resumeButton.SetActive(false);
            pauseText.SetActive(false);
            quitButton.SetActive(false);
        }
    }
}