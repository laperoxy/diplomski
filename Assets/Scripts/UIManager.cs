using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private void Awake()
    {
        Cursor.visible = true;
    }

    private void Start() {
        if (!NetworkManager.Singleton.StartHost())
        {
            NetworkManager.Singleton.StartClient();
        }
    }

}