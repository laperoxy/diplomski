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
    public GameObject on_off_button;
    private void Awake()
    {
        on_off_button.GetComponent<TextMeshProUGUI>().text = "OFFLINE";
        if (File.Exists(Application.dataPath + SAVE_FILE_EXTENSION))
        {
            on_off_button.GetComponent<TextMeshProUGUI>().text = "ONLINE";
        }
    }
}
