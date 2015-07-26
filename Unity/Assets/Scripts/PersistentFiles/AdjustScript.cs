using UnityEngine;
using System.Collections;

public class AdjustScript : MonoBehaviour {
	void OnGUI() {
		if (GUI.Button (new Rect (10, 100, 100, 30), "Money up"))
		{
			GameControl.control.playerData.Money += 10;
			GameControl.control.playerData.Username = "liang";
			GameControl.control.petData.Petname = "zebi";
		}
		if (GUI.Button (new Rect(10, 140, 100, 30), "Money down"))
		{
			GameControl.control.addMoney(-10);
		}
		if (GUI.Button (new Rect (10, 180, 100, 30), "Save")) {
			GameControl.control.Save();
		}
		if (GUI.Button (new Rect(10, 220, 100, 30), "Load")) {
			GameControl.control.Load();
		}
	}
}
