using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class PauseSettings : MonoBehaviour
{
    
    [SerializeField] private GameObject muteIcon;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Toggle toggle = null;
    
    private float my_volume;
    private bool my_isFullscreen;
    private int my_resolutionIndex;
    private int my_qualityIndex;
    private readonly string SAVE_FILE_EXTENSION = "/save.txt";
    
    private string json;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        if (File.Exists(Application.dataPath + SAVE_FILE_EXTENSION))
        {
            string saveString = File.ReadAllText(Application.dataPath + SAVE_FILE_EXTENSION);
            SaveObject loadedSavedSettings = JsonUtility.FromJson<SaveObject>(saveString);
            
            SetVolume(loadedSavedSettings.volume);

            SetFullScreen(loadedSavedSettings.isFullscreen);

            my_resolutionIndex = loadedSavedSettings.resolutionIndex;
            my_qualityIndex = loadedSavedSettings.qualityIndex;


            Debug.Log(saveString);
        }
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
