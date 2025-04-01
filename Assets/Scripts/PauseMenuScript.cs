using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenuScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public SceneChangeScript sceneChangeScript;

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
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
        Debug.Log("MasterVolume: " + PlayerPrefs.GetFloat("Volume"));

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
