using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {

    public Dropdown MethodDropdown;

    public Dropdown HostDropdown;

    public Button SendButton;

    public InputField UrlField;

    public Text ConsoleText;

    public ApiRequest ApiRequest = new ApiRequest();


    // Use this for initialization
    void Start () {
        string response = "";
        UrlField.GetComponent<InputField>().text = "http://localhost:3000/api/";
        SendButton.onClick.AddListener(delegate
        {
            StartCoroutine(ApiRequest.RequestApi(MethodDropdown.options[MethodDropdown.value].text, UrlField.GetComponent<InputField>().text, (res) =>
                {
                    response = res;
                    Debug.Log(response);
                    ConsoleText.text = response;
                })
            );
            
        });
    }
	
    void HostValueChange(Dropdown host)
    {
        Debug.Log(host.options[host.value].text);
        switch (host.options[host.value].text)
        {
            case "LOCAL":
                {
                    UrlField.GetComponent<InputField>().text = "http://localhost:3000/api/";
                    break;
                }
            case "AWS":
                {
                    UrlField.GetComponent<InputField>().text = "http://52.25.129.106:8080/api/";
                    break;
                }
        }
    }
}
