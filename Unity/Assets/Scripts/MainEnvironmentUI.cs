using UnityEngine;
using System.Collections;

public class MainEnvironmentUI : MonoBehaviour {
	public GameObject buildingPanel; // Assign in inspector
	private bool isShowing = false;

	void Update () {
	}
	
	public void SetIsShowing (bool value)
	{
		isShowing = value;
		buildingPanel.SetActive (value);
	}

}