using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebPost: MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LogIn("petar", "password"));
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
            string results = www.downloadHandler.text;
            
            Debug.Log(results);
        }
        
        www.Dispose();
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
    
    IEnumerator ExitGame(string username, string password)
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
}
