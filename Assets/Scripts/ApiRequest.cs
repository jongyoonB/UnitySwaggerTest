using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text;

[System.Serializable]
public class User
{
    public int id;
    public string name;
    public string password;
}

public class ApiRequest {

    public User[] userInstance;
    private string json;
    // Use this for initialization
    void Start () {
        User userToJson = new User();
        userToJson.id = 135;
        userToJson.name = "meme";
        userToJson.password = "pwdpwd";

        json = JsonUtility.ToJson(userToJson);
        //Debug.Log(json);
	}

    public string SendRequest(MonoBehaviour mono, string method, string url)
    {
        string response;
        Debug.Log(method);
        Debug.Log(url);
        switch(method){
            case "GET":
                {
                    Debug.Log("!!");
                    Debug.Log(mono.StartCoroutine(Get(url,userInstance)));
                    break;
                }
            case "POST":
                {
                    mono.StartCoroutine(Post(url, json));
                    break;
                }
            case "PUT":
                {
                    mono.StartCoroutine(Put(url, json));

                    break;
                }
            case "DELETE":
                {
                    //StartCoroutine(Delete(url, json));
                    break;
                }
        }
        return "";
    }

    public IEnumerator Get<T> (string url, T[] array) {
        var request = new UnityWebRequest();
        request.downloadHandler = new DownloadHandlerBuffer();
        request.url = url;
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        request.method = UnityWebRequest.kHttpVerbGET;
        yield return request.Send();

        if(request.isError) {
            Debug.Log(request.error);
        }
        else {
            if (request.responseCode == 200) {
                Debug.Log ("success");
                Debug.Log ("--------------------------------");
                Debug.Log(request.downloadHandler.text);
                Debug.Log ("--------------------------------");
                array = JsonHelper.getJsonArray<T>(request.downloadHandler.text);
                yield return request.downloadHandler.text.ToString();
            } else {
                Debug.Log ("failed");
            }
        }
    }
    public IEnumerator Put(string url, string json)
	{
		var request = new UnityWebRequest();
		request.downloadHandler = new DownloadHandlerBuffer();
		request.url = url;
		byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
		request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
		request.method = UnityWebRequest.kHttpVerbPUT;
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
			}
			else
			{
				Debug.Log("failed");
			}
		}
	}
    public IEnumerator Post(string url, string json)
	{
		var request = new UnityWebRequest();
		request.downloadHandler = new DownloadHandlerBuffer();
		request.url = url;
		byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
		request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
		request.method = UnityWebRequest.kHttpVerbPOST;
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
			}
			else
			{
				Debug.Log("failed");
			}
		}
	}
}