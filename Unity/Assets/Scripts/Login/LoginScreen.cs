using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System;

public class LoginScreen : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void Login (string email, string password)
	{
		WWWForm form = new WWWForm ();
		form.AddField ("name", email);
		form.AddField ("password", password);
		WWW www = new WWW ("http://localhost:8080/api/authenticate", form);
		// WWW www = new WWW ("http://zebibits.com:8080/login", form);		
		Debug.Log ("Testing...");
		StartCoroutine (WaitForRequest (www));
		
		
		// node = JSON.Parse (jsonStream);
		// money = Convert.ToInt32 (node ["money"]);
	}
	
	IEnumerator WaitForRequest (WWW www)
	{
		yield return www;
		// Debug.Log ("Request received...");
		// check for errors
		Text textErrorMsg = GameObject.Find ("TextErrorMsg").GetComponent<Text> ();
		if (www.error == null) {
			// this only checks if there was an error with the http request, not the message itself
			
			// save auth-token and go to main screen
			JSONNode response = JSON.Parse (www.text);
			if (String.Compare (response ["success"], "true") == 0) {
				Debug.Log (response ["token"]);
				textErrorMsg.text = response ["token"];
				textErrorMsg.color = Color.green;
			} else {
				Debug.Log (response ["message"]);
				textErrorMsg.text = response ["message"];
				textErrorMsg.color = Color.red;
			}
		} else {
			// change ErrorMsg to login error
			Debug.Log ("WWW Error: " + www.error);
		}
	}
	
	public void OnLoginClicked ()
	{
		Debug.Log ("Login button clicked!");
		InputField[] inputs = GameObject.Find ("LoginCanvas").GetComponentsInChildren<InputField> ();
		string email = inputs [0].text;
		string password = inputs [1].text;
		if (email == null) {
			email = "";
		}
		if (password == null) {
			password = "";
		}
		Login (email, password);
	}
}
