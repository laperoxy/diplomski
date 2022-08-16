using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConnectionScript : MonoBehaviour
{
    private readonly string SAVE_FILE_EXTENSION = "/credentials.txt";
    public GameObject on_off_tag;
    public GameObject offlineTag;
    
    public GameObject InputPanel;
    public GameObject PlayerPanel;
    private void Start()
    {
        on_off_tag.GetComponent<TextMeshProUGUI>().text = "OFFLINE";
        offlineTag.SetActive(true);
        if (File.Exists(Application.dataPath + SAVE_FILE_EXTENSION))
        {
            string savedCredentials = File.ReadAllText(Application.dataPath + SAVE_FILE_EXTENSION);
            LoginData loadedLoginData = JsonUtility.FromJson<LoginData>(savedCredentials);
            StartCoroutine(WebPost.TokenLogIn(loadedLoginData.Username, loadedLoginData.Token, InputPanel,
                PlayerPanel, on_off_tag, offlineTag));
        }
    }
}
