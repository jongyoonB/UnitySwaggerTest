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

public class RestClient : MonoBehaviour {

    public User[] userInstance;
    public string json;
    // Use this for initialization
    void Start () {
        User userToJson = new User();
        userToJson.id = 135;
        userToJson.name = "meme";
        userToJson.password = "pwdpwd";

        json = JsonUtility.ToJson(userToJson);
        Debug.Log(json);
	}

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp("q"))
        {
            Debug.Log ("PUT /user");
            StartCoroutine(Put("http://localhost:3000/api/user", json));
        }

		if (Input.GetKeyUp("w"))
		{
			Debug.Log("POST /user");
			StartCoroutine(Post("http://localhost:3000/api/user", json));
		}

		if (Input.GetKeyUp("e"))
		{
			Debug.Log("GET /user/{username}");
            Debug.Log(StartCoroutine(Get("http://localhost:3000/api/monsters", userInstance)));
		}
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
                yield return array;
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