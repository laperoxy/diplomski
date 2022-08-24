using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using TMPro;

public class WebPost : MonoBehaviour
{
    private static readonly string SAVE_FILE_EXTENSION = "/credentials.txt";

    public static IEnumerator LogIn(string username, string password, GameObject inputPanel, GameObject playerPanel,
        GameObject on_off_tag, GameObject offlineTag)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/login", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            on_off_tag.GetComponent<TextMeshProUGUI>().text = "OFFLINE";
            offlineTag.SetActive(true);
        }
        else
        {
            string result = www.downloadHandler.text;
            LoginData loginData = ExtractLoginDataFromResult(username, result);
            SaveLoginData(loginData);

            Debug.Log(result);

            inputPanel.SetActive(false);
            playerPanel.SetActive(true);
            offlineTag.SetActive(false);
            on_off_tag.GetComponent<TextMeshProUGUI>().text = "ONLINE";
        }

        www.Dispose();
    }
    
    public static IEnumerator GetTop10Players()
    {
        WWWForm form = new WWWForm();

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/top10", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string result = www.downloadHandler.text;
            Debug.Log(result);
            Top10PlayerData[] top10Players = ExtractTop10PlayerDataFromResult(result);
            Debug.Log(top10Players.Length);
        }

        www.Dispose();
    }

    private static LoginData ExtractLoginDataFromResult(string username, string result)
    {
        TokenTimeAchievementResponse tokenTimeAchievementResponse =
            JsonUtility.FromJson<TokenTimeAchievementResponse>(result);
        return new LoginData(username, tokenTimeAchievementResponse);
    }
    
    private static Top10PlayerData[] ExtractTop10PlayerDataFromResult(string result)
    {
        return JsonUtility.FromJson<Wrapper<Top10PlayerData>>(result).Items;
    }
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    public static IEnumerator Register(string username, string password, GameObject inputPanel, GameObject playerPanel,
        GameObject on_off_tag, GameObject offlineTag)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/register", form);

        yield return www.SendWebRequest();

        // klasican registration flow, ako je fail uzet je username, u suprotnom sve okej
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            on_off_tag.GetComponent<TextMeshProUGUI>().text = "OFFLINE";
            offlineTag.SetActive(true);
        }
        else
        {
            string result = www.downloadHandler.text;
            LoginData loginData = ExtractLoginDataFromResult(username, result);
            SaveLoginData(loginData);

            Debug.Log(result);

            offlineTag.SetActive(false);
            on_off_tag.GetComponent<TextMeshProUGUI>().text = "ONLINE";

            inputPanel.SetActive(false);
            playerPanel.SetActive(true);
        }

        www.Dispose();
    }

    public static IEnumerator ExitGame(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/exit", form);

        yield return www.SendWebRequest();

        www.Dispose();
    }
    
    public static IEnumerator StartGame(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/start", form);

        yield return www.SendWebRequest();

        www.Dispose();
    }
    
    public static IEnumerator WonGame(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/won", form);

        yield return www.SendWebRequest();

        www.Dispose();
    }

    public static IEnumerator TokenLogIn(string username, string token, GameObject inputPanel, GameObject playerPanel,
        GameObject on_off_tag, GameObject offlineTag)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("token", token);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/token", form);

        yield return www.SendWebRequest();

        // ako rezultat nije 200 (uspesan) ovde je potrebno da se korisnik uloguje (ili registruje), u suprotnom 
        // mu se vec zna username i moze da pocne novu igru
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            on_off_tag.GetComponent<TextMeshProUGUI>().text = "OFFLINE";
            offlineTag.SetActive(true);
        }
        else
        {
            string result = www.downloadHandler.text;
            TimePlayedAchievementInstance timePlayedAchievements =
                JsonUtility.FromJson<TimePlayedAchievementInstance>(result);
            LoginData loginData = new LoginData(username, token, timePlayedAchievements.TimePlayed,
                timePlayedAchievements.Achievements, timePlayedAchievements.GamesPlayed);
            SaveLoginData(loginData);
            Debug.Log(result);
            inputPanel.SetActive(false);
            playerPanel.SetActive(true);
            offlineTag.SetActive(false);
            on_off_tag.GetComponent<TextMeshProUGUI>().text = "ONLINE";
        }

        www.Dispose();
    }

    public static void SaveLoginData(LoginData loginData)
    {
        string loginDataJSON = JsonUtility.ToJson(loginData);
        File.WriteAllText(Application.dataPath + SAVE_FILE_EXTENSION, loginDataJSON);
    }
}