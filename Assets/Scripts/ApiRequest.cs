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

[System.Serializable]
public class Monster
{
    public int id;
    public int rank;
    public string name;
    public string type;
}

public class ApiRequest
{
    public User[] userInstance;

    public Monster[] monsterInstance;

    private string json;

    public IEnumerator RequestApi(string method, string url, System.Action<string> callback)
    {
        string instanceName = "";
        string consoleText = "";
        consoleText = "Resquest : " + method + " / " + url + "\n\nResponse\n";
        int index;

        index = url.LastIndexOf('/');
        instanceName = url.Substring(index + 1);

        var request = new UnityWebRequest();
        request.downloadHandler = new DownloadHandlerBuffer();
        request.url = url;
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        if (method.Equals("POST") || method.Equals("PUT"))
        {
            if (instanceName.Equals("user"))
            {
                User userJsonData = new User();

                userJsonData.id = 2;
                userJsonData.name = "HOGE2";
                userJsonData.password = "pwd";
                json = JsonUtility.ToJson(userJsonData);
            }
            else
            {
                Monster monsterJsonData = new Monster();

                monsterJsonData.id = 3;
                monsterJsonData.name = "HOGE3";
                monsterJsonData.type = "Light";
                monsterJsonData.rank = 3;
                json = JsonUtility.ToJson(monsterJsonData);
            }
            

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
                    request.method = UnityWebRequest.kHttpVerbPOST;
                    break;
                }
            case "PUT":
                {
                    request.method = UnityWebRequest.kHttpVerbPUT;
                    break;
                }
            case "DELETE":
                {
                    request.method = UnityWebRequest.kHttpVerbDELETE;
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
                consoleText += request.downloadHandler.text;
                yield return null;
                switch (instanceName)
                {
                    case "user":
                        {
                            consoleText += "\n\nUser Instance\n";
                            userInstance = JsonHelper.jsonToInstance<User>(request.downloadHandler.text);

                            for (int i = 0; i < userInstance.Length; i++)
                            {
                                //Debug.Log(userInstance[i].id);
                                //Debug.Log(userInstance[i].name);
                                //Debug.Log(userInstance[i].password);
                                consoleText += "\nID : " + userInstance[i].id.ToString() + "\n";
                                consoleText += "name : " + userInstance[i].name.ToString() + "\n";
                                consoleText += "password : " + userInstance[i].password.ToString() + "\n";
                            }
                            break;
                        }
                    case "monster":
                        {
                            consoleText += "\n\nMonster Instance\n";
                            monsterInstance = JsonHelper.jsonToInstance<Monster>(request.downloadHandler.text);

                            for (int i = 0; i < monsterInstance.Length; i++)
                            {
                                consoleText += "\nID : " + monsterInstance[i].id.ToString() + "\n";
                                consoleText += "name : " + monsterInstance[i].name.ToString() + "\n";
                                consoleText += "type : " + monsterInstance[i].type.ToString() + "\n";
                                consoleText += "rank : " + monsterInstance[i].rank.ToString() + "\n";
                            }
                            break;
                        }
                }
                callback(consoleText);
            }
            else
            {
                Debug.Log("failed");
            }
        }
    }
}