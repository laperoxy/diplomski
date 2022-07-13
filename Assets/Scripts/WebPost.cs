using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebPost: MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PostData("petar", "password"));
    }
    
    IEnumerator PostData(string username, string password)
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
}
