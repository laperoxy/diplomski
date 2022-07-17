using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class WebPost: MonoBehaviour
{

    private string my_username;
    private string my_token;
    private readonly string SAVE_FILE_EXTENSION = "/credentials.txt";
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
    
    IEnumerator LogIn(string username, string password)
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

    private LoginData ExtractLoginDataFromResult(string username, string result)
    {
        TokenTimeResponse tokenTimeResponse = JsonUtility.FromJson<TokenTimeResponse>(result);
        return new LoginData(username,tokenTimeResponse);
    }

    IEnumerator Register(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/register", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string results = www.downloadHandler.text;
            
            Debug.Log(results);
        }
        
        www.Dispose();
    }
    
    IEnumerator ExitGame(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/exit", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string results = www.downloadHandler.text;
            
            Debug.Log(results);
        }
        
        www.Dispose();
    }
    IEnumerator TokenLogIn(string username, string token)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("token", token);
        
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/token", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string results = www.downloadHandler.text;
            
            Debug.Log(results);
        }
        
        www.Dispose();
    }
    
    public void SaveLoginData(LoginData loginData)
    {
        string loginDataJSON = JsonUtility.ToJson(loginData);
        File.WriteAllText(Application.dataPath + SAVE_FILE_EXTENSION, loginDataJSON);
    }
}
