using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject muteIcon;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Toggle toggle = null;
    [SerializeField] private TMP_Dropdown resolutionDropwdown = null;
    [SerializeField] private TMP_Dropdown graphicsDropdown = null;
    private Resolution[] resolutions;

    private float my_volume;
    private bool my_isFullscreen;
    private int my_resolutionIndex;
    private int my_qualityIndex;
    private readonly string SAVE_FILE_EXTENSION = "/settings.json";

    private string json;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropwdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; ++i)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropwdown.AddOptions(options);
        resolutionDropwdown.value = resolutions.Length - 1;
        resolutionDropwdown.RefreshShownValue();

        LoadSettings();
    }

    private void LoadSettings()
    {
        if (File.Exists(Application.dataPath + SAVE_FILE_EXTENSION))
        {
            string saveString = File.ReadAllText(Application.dataPath + SAVE_FILE_EXTENSION);
            SaveObject loadedSavedSettings = JsonUtility.FromJson<SaveObject>(saveString);
            SetVolume(loadedSavedSettings.volume);

            resolutionDropwdown.value = loadedSavedSettings.resolutionIndex;
            resolutionDropwdown.RefreshShownValue();
            SetResolution(loadedSavedSettings.resolutionIndex);

            SetFullScreen(loadedSavedSettings.isFullscreen);

            graphicsDropdown.value = loadedSavedSettings.qualityIndex;
            graphicsDropdown.RefreshShownValue();
            SetQuality(loadedSavedSettings.qualityIndex);

            //Debug.Log(saveString);
        }
    }

    public void SetResolution(int resulutionIndex)
    {
        Resolution resolution = resolutions[resulutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        my_resolutionIndex = resulutionIndex;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeSlider.value = volume;
        if (volume == 0)
        {
            muteIcon.SetActive(true);
        }
        else
        {
            muteIcon.SetActive(false);
        }

        my_volume = volume;
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
        my_isFullscreen = isFullscreen;
        toggle.isOn = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        my_qualityIndex = qualityIndex;
    }

    public void SaveBeforeReturn()
    {
        SaveObject saveObject = new SaveObject
        {
            volume = my_volume,
            isFullscreen = my_isFullscreen,
            resolutionIndex = my_resolutionIndex,
            qualityIndex = my_qualityIndex
        };

        json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + SAVE_FILE_EXTENSION, json);
    }

    private class SaveObject
    {
        public float volume;
        public bool isFullscreen;
        public int resolutionIndex;
        public int qualityIndex;
    }
}