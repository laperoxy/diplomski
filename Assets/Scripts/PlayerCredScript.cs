using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PlayerCredScript : MonoBehaviour
{
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
            string loadedCredentials = File.ReadAllText(Application.dataPath + SAVE_FILE_EXTENSION);
            LoginData loadedLoginData = JsonUtility.FromJson<LoginData>(loadedCredentials);

            username.GetComponent<TextMeshProUGUI>().text = loadedLoginData.Username;
            timePlayed.GetComponent<TextMeshProUGUI>().text = Convert.ToString(loadedLoginData.TimePlayed);
            achievements.GetComponent<TextMeshProUGUI>().text = Convert.ToString(loadedLoginData.Achievements);
            Convert.ToString(loadedLoginData.GamesPlayed);

            long myAchievements = loadedLoginData.Achievements;

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
}
