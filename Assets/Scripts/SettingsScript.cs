using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer Mixer;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private int currentResolutionIndex = 0;
    private double currentRefreshRate;


    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < resolutions.Length; i++)
        {

            filteredResolutions.Add(resolutions[i]);

        }

        List<string> options = new List<string>();
        int index = 0;
        foreach (Resolution item in filteredResolutions)
        {
            string resolutionOption = item.width + "x" + item.height;
            options.Add(resolutionOption);
            if (item.width == Screen.width && item.height == Screen.height)
            {
                currentResolutionIndex = index;
            }
            index++;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {
        
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        Mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
