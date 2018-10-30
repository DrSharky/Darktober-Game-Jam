using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager uiManager;

    public static UIManager instance
    {
        get
        {
            if (!uiManager)
            {
                uiManager = FindObjectOfType(typeof(UIManager)) as UIManager;

                if (!uiManager)
                    Debug.LogError("Needs 1 active UIManager script on a GO in your scene!");
            }
            return uiManager;
        }
    }

    private GameObject canvas;
    [SerializeField]
    private GameObject beginButton;
    [SerializeField]
    private GameObject optionsButton;
    [SerializeField]
    private GameObject creditsButton;
    [SerializeField]
    private GameObject quitButton;
    [SerializeField]
    private GameObject howToPlayButton;
    [SerializeField]
    private GameObject logo;
    [SerializeField]
    private GameObject optionsPanel;
    [SerializeField]
    private GameObject creditsPanel;
    [SerializeField]
    private AudioClip menuMusic;
    [SerializeField]
    private AudioClip gameMusic;
    private Scene activeScene;
    private AudioSource music;
    private Slider musicVolumeSlider;
    private AudioSource effects;
    private Slider effectsVolumeSlider;
    [SerializeField]
    private GameObject howToPlayPanel;


    private float musicVol;
    private float effectsVol;

    //Begin button pressed
    public void BeginClick()
    {
        SceneManager.LoadScene(1);
    }

    private void ToggleMenu()
    {
        logo.SetActive(!logo.activeInHierarchy);
        beginButton.SetActive(!beginButton.activeInHierarchy);
        optionsButton.SetActive(!optionsButton.activeInHierarchy);
        creditsButton.SetActive(!creditsButton.activeInHierarchy);
        quitButton.SetActive(!quitButton.activeInHierarchy);
        howToPlayButton.SetActive(!howToPlayButton.activeInHierarchy);
    }

    public void ToggleOptionsPanel()
    {
        if(activeScene.buildIndex == 0)
        {
            ToggleMenu();
            optionsPanel.SetActive(!optionsPanel.activeInHierarchy);
        }
        else
        {
            if (Time.timeScale == 0.0f)
                Time.timeScale = 1.0f;
            else
                Time.timeScale = 0.0f;
        }
    }

    public void ToggleHowToPlayPanel()
    {
        ToggleMenu();
        howToPlayPanel.SetActive(!howToPlayPanel.activeInHierarchy);
    }

    public void ToggleCredits()
    {
        ToggleMenu();
        creditsPanel.SetActive(!creditsPanel.activeInHierarchy);
    }

    public void QuitClick()
    {
        Application.Quit();
    }

    //Player pressed play button,
    //show reference image, then begin countdown.
    public void Play()
    {
        EventManager.TriggerEvent("GameStart");
    }

    public void OnSceneChange(Scene scene, LoadSceneMode mode)
    {
        activeScene = scene;
        if (scene.buildIndex == 1)
        {
            effects = Camera.main.GetComponent<AudioSource>();
            effects.volume = effectsVol;
            music.clip = gameMusic;
            music.Play();


            Play();
        }
        else
            music.clip = menuMusic;
    }

    public void CloseHowTo()
    {
        howToPlayPanel = GameObject.FindGameObjectWithTag("h2p");
        howToPlayPanel.SetActive(false);
    }

    public void OnMusicVolumeChange()
    {
        float newVol = musicVolumeSlider.value;
        music.volume = newVol;
        PlayerPrefs.SetFloat("MusicVol", newVol);
    }

    public void OnEffectsVolumeChange()
    {
        float newVol = effectsVolumeSlider.value;
        if (activeScene.buildIndex == 1)
            effects.volume = newVol;
        PlayerPrefs.SetFloat("EffectsVol", newVol);
    }


    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        activeScene = SceneManager.GetActiveScene();
        SceneManager.sceneLoaded += OnSceneChange;
        music = GetComponent<AudioSource>();
        musicVolumeSlider = optionsPanel.transform.GetChild(2).GetComponent<Slider>();
        effectsVolumeSlider = optionsPanel.transform.GetChild(4).GetComponent<Slider>();

        musicVol = PlayerPrefs.GetFloat("MusicVol", 0.5f);
        musicVolumeSlider.value = musicVol;
        music.volume = musicVol;

        effectsVol = PlayerPrefs.GetFloat("EffectsVol", 0.5f);
        effectsVolumeSlider.value = effectsVol;

        if (uiManager == null)
            uiManager = this;
        else if (uiManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

}
