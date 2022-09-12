using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class FinishGameScript : MonoBehaviour
{
    public void StopGame()
    {
        Debug.Log("Returning to menu and disconnecting");
        NetworkManager.Singleton.Shutdown(true);
        SceneManager.LoadScene(0);
        //InputSystem.DisableDevice(Keyboard.current);
    }
}
