using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControlScript : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private Slider volumeSlider;

    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        Mixer.SetFloat("MasterVolume", Mathf.Log10(volume)*20);
    }
}
