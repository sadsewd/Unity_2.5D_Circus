using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManagerScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    private float volume;

    public void Awake()
    {
        volume = PlayerPrefs.GetFloat("volume", 1f);
    }
    private void Update()
    {
        if (PauseMenuScript.GameIsPaused)
        {
            audioMixer.SetFloat("MasterVolume", -30f);
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", volume);
        }
    }

}
