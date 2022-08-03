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

    public void RegisterUser()
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

    public void LoginUser()
    {
        username = usernameInput.GetComponent<TextMeshProUGUI>().text;
        password = passwordInput.GetComponent<TextMeshProUGUI>().text;
        if (username.Length < 3 || password.Length < 3)
        {
            inputUI.SetActive(false);
            errorUI.SetActive(true);
        }
    }
}