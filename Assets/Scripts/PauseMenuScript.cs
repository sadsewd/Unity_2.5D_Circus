using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenuScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    private float volume;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public SceneChangeScript sceneChangeScript;

    private void Awake()
    {
        volume = PlayerPrefs.GetFloat("volume", 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        audioMixer.SetFloat("MasterVolume", volume);

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        audioMixer.SetFloat("MasterVolume", -30f);

    }

    public void Quit()
    {
        StartCoroutine(sceneChangeScript.DelayMainMenu());
    }
}
