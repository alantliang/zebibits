using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
	public static UIManager instance = null;

	void Update () {
	}

	public void GoToPetInfoScene() {
		Application.LoadLevel ("PetInfo");
	}

	public void GoToMain() {
		Application.LoadLevel ("Main");
	}
}