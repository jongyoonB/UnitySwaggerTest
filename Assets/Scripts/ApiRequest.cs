using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text;

public class ApiRequest
{

    private string json;
    // Use this for initialization
    void Start()
    {

    }

    public IEnumerator RequestApi(string method, string url, System.Action<string> callback)
    {
        var request = new UnityWebRequest();
        request.downloadHandler = new DownloadHandlerBuffer();
        request.url = url;
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        if (!method.Equals("GET"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        }
        switch (method)
        {
            case "GET":
                {
                    request.method = UnityWebRequest.kHttpVerbGET;
                    break;
                }
            case "POST":
                {
                    request.method = UnityWebRequest.kHttpVerbGET;
                    break;
                }
            case "PUT":
                {
                    request.method = UnityWebRequest.kHttpVerbGET;
                    break;
                }
            case "DELETE":
                {
                    request.method = UnityWebRequest.kHttpVerbGET;
                    break;
                }
        }
        yield return request.Send();

        if (request.isError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                Debug.Log("success");
                Debug.Log("--------------------------------");
                Debug.Log(request.downloadHandler.text);
                Debug.Log("--------------------------------");
                yield return null;
                callback(request.downloadHandler.text);
            }
            else
            {
                Debug.Log("failed");
            }
        }
    }
}