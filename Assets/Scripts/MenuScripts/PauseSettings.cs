using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class PauseSettings : MonoBehaviour
{
    [SerializeField] private GameObject muteIcon;
    [SerializeField] private GameObject Fog;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Toggle toggle = null;
    [SerializeField] private Toggle fogToggle = null;
    private Resolution[] resolutions;
    
    private float my_volume;
    private bool my_isFullscreen;
    private bool my_isfogOn;
    private int my_resolutionIndex;
    private int my_qualityIndex;
    
    private int my_screen_width;
    private int my_screen_height;
    
    private readonly string SAVE_FILE_EXTENSION = "/settings.json";

    private string json;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        LoadSettings();
    }

    private void LoadSettings()
    {
        if (File.Exists(Application.dataPath + SAVE_FILE_EXTENSION))
        {
            string saveString = File.ReadAllText(Application.dataPath + SAVE_FILE_EXTENSION);
            SaveObject loadedSavedSettings = JsonUtility.FromJson<SaveObject>(saveString);
            
            my_resolutionIndex = loadedSavedSettings.resolutionIndex;
            my_qualityIndex = loadedSavedSettings.qualityIndex;

            if (loadedSavedSettings.volume > 1f || loadedSavedSettings.volume < 0f)
            {
                SetVolume(0.5f);
            }
            else
            {
                SetVolume(loadedSavedSettings.volume);
            }

            SetFullScreen(loadedSavedSettings.isFullscreen);
            if (!loadedSavedSettings.isFullscreen)
            {
                Screen.SetResolution(loadedSavedSettings.screen_width, loadedSavedSettings.screen_height,
                    Screen.fullScreen);
            }
            
            SetFog(loadedSavedSettings.isFogOn);
            
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
        if (my_isFullscreen)
        {
            Resolution resolution = resolutions[my_resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, true);
        }
    }

    public void SetFog(bool isFogOn)
    {
        Fog.SetActive(isFogOn);
        fogToggle.isOn = isFogOn;
        my_isfogOn = isFogOn;
    }

    public void SaveBeforeReturn()
    {
        
        if (!my_isFullscreen)
        {
            my_screen_width = Screen.width;
            my_screen_height = Screen.height;
        }
        
        SaveObject saveObject = new SaveObject
        {
            volume = my_volume,
            isFullscreen = my_isFullscreen,
            isFogOn = my_isfogOn,
            resolutionIndex = my_resolutionIndex,
            qualityIndex = my_qualityIndex,
            screen_width = my_screen_width,
            screen_height = my_screen_height
        };

        json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + SAVE_FILE_EXTENSION, json);
    }

    public void StopGame()
    {
        Debug.Log("Returning to menu and disconnecting");
        NetworkManager.Singleton.Shutdown(true);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Exiting game and disconnecting");
        NetworkManager.Singleton.Shutdown(true);
        Application.Quit();
    }

    private class SaveObject
    {
        public float volume;
        public bool isFullscreen;
        public bool isFogOn;
        public int resolutionIndex;
        public int qualityIndex;
        public int screen_width;
        public int screen_height;
    }
}