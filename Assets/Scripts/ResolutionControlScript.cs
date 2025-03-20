using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionControl : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private int currentResolutionIndex = 0;
    private double currentRefreshRate;
    private bool isFullscreen = false;
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
            string resolutionOption = item.width + "x" + item.height + " " + item.refreshRateRatio.value + " Hz";
            options.Add(resolutionOption);
            if(item.width == Screen.width && item.height == Screen.height)
            {
                currentResolutionIndex = index;
            }
            index++;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, isFullscreen);
    }

    public void SetFullScreen()
    {
        if (isFullscreen)
        {
            isFullscreen = false;
        }
        else
        {
            isFullscreen = true;
        }
        Screen.fullScreen = !Screen.fullScreen;
    }

}
