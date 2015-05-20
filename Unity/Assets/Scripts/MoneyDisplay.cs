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
		GameObject playerWallet = GameObject.Find ("PlayerWallet");
		PlayerWallet playerWalletScript = (PlayerWallet)playerWallet.GetComponent ("PlayerWallet");
		TextMesh myTextMesh = (TextMesh)GetComponent ("TextMesh");
		myTextMesh.text = "MONEY: " + playerWalletScript.Money.ToString ();
	}
}
