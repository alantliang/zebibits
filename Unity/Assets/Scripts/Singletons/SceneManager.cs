using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
	public static SceneManager instance = null;

	void Awake () {
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	void Update ()
	{
	}

	public void GoToMain ()
	{
		Application.LoadLevel ("Main");
	}

	public void GoToLogin()
	{
		Application.LoadLevel ("Login");
	}

	public void GoToPetInfoScene ()
	{
		Application.LoadLevel ("PetInfo");
	}

	public void GoToBuildingShop ()
	{
		Application.LoadLevel ("BuildingShop");
	}
}