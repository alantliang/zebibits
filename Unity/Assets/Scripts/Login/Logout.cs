using UnityEngine;
using System.Collections;

public class Logout : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void OnLogout ()
	{
		GameControl.control.ClearLocalStorage ();
		SceneManager.instance.GoToLogin ();
	}
}
