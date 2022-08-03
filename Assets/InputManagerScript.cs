using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManagerScript : MonoBehaviour
{
    private string username;
    public GameObject usernameInput;

    private string password;
    public GameObject passwordInput;

    public GameObject inputUI;
    public GameObject errorUI;

    public void StoreCred()
    {
        username = usernameInput.GetComponent<TextMeshProUGUI>().text;
        password = passwordInput.GetComponent<TextMeshProUGUI>().text;
        Debug.Log(username + " " + password);
        if (username.Length < 3 || password.Length < 3)
        {
            inputUI.SetActive(false);
            errorUI.SetActive(true);
        }
    }

    public void GetCred()
    {
        username = usernameInput.GetComponent<TextMeshProUGUI>().text;
        password = passwordInput.GetComponent<TextMeshProUGUI>().text;
        if (username.Equals(null) || password.Equals(null))
        {
            inputUI.SetActive(false);
            errorUI.SetActive(true);
        }
    }
}