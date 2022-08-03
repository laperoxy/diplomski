using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class WebPost: MonoBehaviour
{
    
    private static readonly string SAVE_FILE_EXTENSION = "/credentials.txt";
    void Start()
    {
        if (File.Exists(Application.dataPath + SAVE_FILE_EXTENSION))
        {
            string savedCredentials = File.ReadAllText(Application.dataPath + SAVE_FILE_EXTENSION);
            LoginData loadedLoginData = JsonUtility.FromJson<LoginData>(savedCredentials);
            StartCoroutine(TokenLogIn(loadedLoginData.Username, loadedLoginData.Token));
            Debug.Log(savedCredentials);
        }
    }
    
    public static IEnumerator LogIn(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/login", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string result = www.downloadHandler.text;
            LoginData loginData = ExtractLoginDataFromResult(username, result);
            SaveLoginData(loginData);
            
            Debug.Log(result);
        }
        
        www.Dispose();
    }

    private static LoginData ExtractLoginDataFromResult(string username, string result)
    {
        TokenTimeAchievementResponse tokenTimeAchievementResponse = JsonUtility.FromJson<TokenTimeAchievementResponse>(result);
        return new LoginData(username,tokenTimeAchievementResponse);
    }

    public static IEnumerator Register(string username, string password)
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
        }
        else
        {
            string result = www.downloadHandler.text;
            LoginData loginData = ExtractLoginDataFromResult(username, result);
            SaveLoginData(loginData);
            
            Debug.Log(result);
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
    public static IEnumerator TokenLogIn(string username, string token)
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
        }
        else
        {
            string result = www.downloadHandler.text;
            TimePlayedAchievementInstance timePlayedAchievements = JsonUtility.FromJson<TimePlayedAchievementInstance>(result);
            LoginData loginData = new LoginData(username, token, timePlayedAchievements.TimePlayed, timePlayedAchievements.Achievements);
            SaveLoginData(loginData);
            Debug.Log(result);
        }
        
        www.Dispose();
    }
    
    public static void SaveLoginData(LoginData loginData)
    {
        string loginDataJSON = JsonUtility.ToJson(loginData);
        File.WriteAllText(Application.dataPath + SAVE_FILE_EXTENSION, loginDataJSON);
    }
}
