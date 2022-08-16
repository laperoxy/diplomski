using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManagerScript : MonoBehaviour
{
    private string username;
    public GameObject usernameInput;

    private string password;
    public GameObject passwordInput;

    public GameObject inputUI;
    public GameObject errorUI;

    public GameObject returnButton;

    public GameObject InputPanel;
    public GameObject PlayerPanel;

    public GameObject on_off_tag;
    public GameObject offlineTag;

    private readonly string SAVE_FILE_EXTENSION = "/credentials.txt";

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
            StartCoroutine(WebPost.Register(username, password, InputPanel, PlayerPanel, on_off_tag, offlineTag));
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
            StartCoroutine(WebPost.LogIn(username, password, InputPanel, PlayerPanel, on_off_tag, offlineTag));
        }
    }
}