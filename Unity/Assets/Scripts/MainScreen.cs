using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

	public static MainScreen instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.

	void Awake ()
	{
		//Check if there is already an instance of MainScreen
		if (instance == null)
			//if not, set it to this.
			instance = this;
		//If instance already exists:
		else if (instance != this)
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);
		
		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
