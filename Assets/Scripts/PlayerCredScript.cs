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

    public GameObject leaf1;
    public GameObject leaf2;
    public GameObject crown;
    
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

            int myAchievements = int.Parse(loadedCredentials.Achievements);

            if (myAchievements >= 10 && myAchievements < 20)
            {
                leaf1.SetActive(true);
            }else if (myAchievements >= 20 && myAchievements < 30)
            {
                leaf1.SetActive(false);
                leaf2.SetActive(true);
            }else if (myAchievements >= 30)
            {
                leaf2.SetActive(false);
                crown.SetActive(true);
            }
            else
            {
                leaf1.SetActive(false);
                leaf2.SetActive(false);
                crown.SetActive(false);
            }
            

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
