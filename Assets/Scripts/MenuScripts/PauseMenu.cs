using Unity.Netcode;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI = null;
    [SerializeField] private GameObject settingsMenuUI = null;

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
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game and disconnecting");
        NetworkManager.Singleton.Shutdown(true);
        Application.Quit();
    }
}