using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject muteIcon;
    [SerializeField] private Slider volumeSlider = null;


    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        if (volume == 0)
        {
            muteIcon.SetActive(true);
        }
        else
        {
            muteIcon.SetActive(false);
        }
    }

    public void VolumeButton()
    {
        if (muteIcon.activeSelf)
        {
            volumeSlider.value = 0.5f;
            muteIcon.SetActive(false);
        }
        else
        {
            volumeSlider.value = 0f;
            muteIcon.SetActive(true);
        }
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}