using System.IO;
using TMPro;
using UnityEngine;

public class InputManagerScript : MonoBehaviour
{
    private string username;
    public GameObject usernameInput;

    private string password;
    public GameObject passwordInput;

    public GameObject inputUI;
    public GameObject errorUI;

    public GameObject InputPanel;
    public GameObject PlayedPanel;

    public GameObject returnButton;

    private readonly string SAVE_FILE_EXTENSION = "/credentials.txt";

    void Start()
    {
        if (File.Exists(Application.dataPath + SAVE_FILE_EXTENSION))
        {
            InputPanel.SetActive(false);
            PlayedPanel.SetActive(true);
        }
    }

    public void RegisterUser()
    {
        username = usernameInput.GetComponent<TextMeshProUGUI>().text;
        password = passwordInput.GetComponent<TextMeshProUGUI>().text;
        Debug.Log(username + " " + password);
        if (username.Length < 3 || password.Length < 3)
        {
            inputUI.SetActive(false);
            returnButton.SetActive(false);
            errorUI.SetActive(true);
        }
        else
        {
            StartCoroutine(WebPost.Register(username, password));
        }
    }

    public void LoginUser()
    {
        username = usernameInput.GetComponent<TextMeshProUGUI>().text;
        password = passwordInput.GetComponent<TextMeshProUGUI>().text;
        if (username.Length < 3 || password.Length < 3)
        {
            inputUI.SetActive(false);
            returnButton.SetActive(false);
            errorUI.SetActive(true);
        }
        else
        {
            StartCoroutine(WebPost.LogIn(username, password));
        }
    }
}