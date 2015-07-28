using UnityEngine;
using System.Collections;

public class MoneyDisplay : MonoBehaviour
{	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		// this should listen to a OnMoneyChangedEvent
		TextMesh myTextMesh = (TextMesh)GetComponent ("TextMesh");
		myTextMesh.text = "MONEY: " + GameControl.control.playerData.Money;
	}
}
