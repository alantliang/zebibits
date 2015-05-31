using UnityEngine;
using System.Collections;

public class LoginScreen : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Login ("xtraneus@gmail.com", "taota0");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void Login (string username, string password)
	{
		WWWForm form = new WWWForm ();
		form.AddField ("username", "xtraneus@gmail.com");
		form.AddField ("password", "taota0");
		WWW www = new WWW ("http://localhost:8080/login", form);
		// WWW www = new WWW ("http://zebibits.com:8080/login", form);		
		Debug.Log ("Testing...");
		StartCoroutine (WaitForRequest (www));
		
		
		// node = JSON.Parse (jsonStream);
		// money = Convert.ToInt32 (node ["money"]);
	}
	
	IEnumerator WaitForRequest (WWW www)
	{
		Debug.Log ("Waiting for request...");
		yield return www;
		Debug.Log ("Request received...");
		Debug.Log ("Done");
		// check for errors
		if (www.error == null) {
			Debug.Log ("WWW Ok!: " + www.text);
		} else {
			Debug.Log ("WWW Error: " + www.error);
		}
	}
}
