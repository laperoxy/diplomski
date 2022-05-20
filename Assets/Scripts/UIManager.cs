using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button startServerButton;
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;

    private void Awake()
    {
        Cursor.visible = true;
    }

    private void Start()
    {
        startHostButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost()) DisableAllButtons();
        });

        startServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer()) DisableAllButtons();
        });

        startClientButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient()) DisableAllButtons();
        });
    }

    private void DisableAllButtons()
    {
        startHostButton.gameObject.SetActive(false);
        startServerButton.gameObject.SetActive(false);
        startClientButton.gameObject.SetActive(false);
    }
}