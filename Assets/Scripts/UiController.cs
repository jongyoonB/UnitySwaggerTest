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
        UrlField.GetComponent<InputField>().text = "http://localhost:3000/api/";

        SendButton.onClick.AddListener(delegate
        {
            ConsoleText.text = ApiRequest.SendRequest(this, MethodDropdown.options[MethodDropdown.value].text, UrlField.GetComponent<InputField>().text);
            Debug.Log(ApiRequest.SendRequest(this, MethodDropdown.options[MethodDropdown.value].text, UrlField.GetComponent<InputField>().text));
        });

        MethodDropdown.onValueChanged.AddListener(delegate
        {
            MethodValueChange(MethodDropdown);
        });

         HostDropdown.onValueChanged.AddListener(delegate
         {
             HostValueChange(HostDropdown);
         });
    }
	
	// Update is called once per frame
	void Update () {

    }

    void MethodValueChange(Dropdown method)
    {
        Debug.Log(method.options[method.value].text);
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
