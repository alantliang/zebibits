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
		GameControl.control.Load ();
		if (GameControl.control.IsLoggedIn ()) {
			SceneManager.instance.GoToMain ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void Login (string email, string password)
	{
		// Login to website
		WWWForm form = new WWWForm ();
		form.AddField ("name", email);
		form.AddField ("password", password);
		WWW www = new WWW ("http://zebibits.com:1337/api/authenticate", form);
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
				// successful http request
				string token = response ["token"];
				saveToken (token);
				SceneManager.instance.GoToMain ();
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

	private void saveToken (string token)
	{
		Debug.Log ("LoginScreen: saveToken");
		GameControl.control.playerData.AuthToken = token;
		Debug.Log ("LoginScreen: saveToken: set token");
		GameControl.control.Save ();
		Debug.Log (token);
		Text textErrorMsg = GameObject.Find ("TextErrorMsg").GetComponent<Text> ();
		textErrorMsg.text = token;
		textErrorMsg.color = Color.green;
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
