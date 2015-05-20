using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;
using System;

public class PlayerWallet : MonoBehaviour
{
	private JSONNode node;
	private int money;
	public int Money {
		get { return money; }
	}
	// Use this for initialization
	void Start ()
	{
		string jsonStream = "";
		using (StreamReader sr = new StreamReader(GetPlayerStatsFilepath ())) {
			string line;
			while ((line = sr.ReadLine()) != null) {
				jsonStream += line;
			}
		}
		
		node = JSON.Parse (jsonStream);
		money = Convert.ToInt32 (node ["money"]);
	}
	
	string GetPlayerStatsFilepath ()
	{
		return Path.GetFullPath (Path.Combine (Application.streamingAssetsPath, "PlayerStats.json"));
	}
	
	public void AddMoney (int amt)
	{
		money += amt;
		node ["money"] = money.ToString ();
		SaveMoney (money);
	}
	
	void SaveMoney (int amt)
	{
		File.WriteAllText (GetPlayerStatsFilepath (), node.ToString ());
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
