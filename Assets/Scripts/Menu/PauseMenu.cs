using System.IO;
using Unity.Netcode;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI = null;
    [SerializeField] private GameObject settingsMenuUI = null;

    private readonly string SAVE_FILE_EXTENSION = "/credentials.txt";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !settingsMenuUI.activeSelf)
        {
            if (gameIsPaused)
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
        //settingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        if (!File.Exists(Application.dataPath + SAVE_FILE_EXTENSION))
        {
            Time.timeScale = 0f;
        }
        gameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game and disconnecting");
        NetworkManager.Singleton.Shutdown(true);
        Application.Quit();
    }
}