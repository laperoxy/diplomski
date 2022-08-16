using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PlayerCredScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    private readonly string SAVE_FILE_EXTENSION = "/credentials.txt";

    public GameObject username;
    public GameObject timePlayed;
    public GameObject achievements;
    
    void Start()
    {
        LoadCredentials();
    }

    private void LoadCredentials()
    {
        if (File.Exists(Application.dataPath + SAVE_FILE_EXTENSION))
        {
            string saveString = File.ReadAllText(Application.dataPath + SAVE_FILE_EXTENSION);
            SaveObject loadedCredentials = JsonUtility.FromJson<SaveObject>(saveString);

            username.GetComponent<TextMeshProUGUI>().text = loadedCredentials.Username;
            timePlayed.GetComponent<TextMeshProUGUI>().text = loadedCredentials.TimePlayed;
            achievements.GetComponent<TextMeshProUGUI>().text = loadedCredentials.Achievements;
            

        }
    }

    private class SaveObject
    {
        public string Username;
        public string Token;
        public string TimePlayed;
        public string Achievements;
    }
    
}
