using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private Toggle fullscreenToggle;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private int currentResolutionIndex = 0;
    private double currentRefreshRate;

    private void Awake()
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel"));
        qualityDropdown.value = PlayerPrefs.GetInt("QualityLevel");
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            Mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 1f);
            volumeSlider.value = 1f;
        }
        if (!PlayerPrefs.HasKey("ResolutionWidth") && !PlayerPrefs.HasKey("ResolutionHeight") && !PlayerPrefs.HasKey("Fullscreen"))
        {
            PlayerPrefs.SetInt("ResolutionWidth", Screen.currentResolution.width);
            PlayerPrefs.SetInt("ResolutionHeight", Screen.currentResolution.height);
            PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
        }
        else
        {
            Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth"), PlayerPrefs.GetInt("ResolutionHeight"), PlayerPrefs.GetInt("Fullscreen") == 1 ? true : false);
        }
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            if (PlayerPrefs.GetInt("Fullscreen") == 1)
            {
                fullscreenToggle.isOn = true;
            }
            else
            {
                fullscreenToggle.isOn = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
            fullscreenToggle.isOn = Screen.fullScreen;
        }
        
        
    }

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
            if (!PlayerPrefs.HasKey("ResolutionWidth") && !PlayerPrefs.HasKey("ResolutionHeight")) 
            {
                if (item.width == Screen.width && item.height == Screen.height)
                {
                    currentResolutionIndex = index;
                }
                index++;
            }
            else
            {
                if (item.width == PlayerPrefs.GetInt("ResolutionWidth") && item.height == PlayerPrefs.GetInt("ResolutionHeight"))
                {
                    currentResolutionIndex = index;
                }
                index++;
            }
        }

               

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionWidth", resolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", resolution.height);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }


    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        Mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }
}
