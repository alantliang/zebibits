using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// keep a singleton object to hold all of our information
public class GameControl : MonoBehaviour
{

	// stactic variable to access this object faster
	public static GameControl control;

	public PlayerData playerData = new PlayerData ();
	public PetData petData = new PetData ();
	public bool testDisplay = true;

	// Use this for initialization
	void Awake ()
	{
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}

		// force a different code path in the BinaryFormatter that doesn't rely on run-time code generation which breaks in ios
		Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
	}
	
	void OnGUI ()
	{
		// for testing purposes
		if (testDisplay) {
			GUI.Label (new Rect (10, 10, 100, 30), "Money: " + playerData.Money);
			GUI.Label (new Rect (10, 30, 100, 30), "UserId: " + playerData.Username);
			GUI.Label (new Rect (10, 50, 100, 30), "Petname: " + petData.Petname);
		}
	}

	public void addMoney (float amt)
	{
		playerData.Money += amt;
	}

	public void Save ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		AllData allData = new AllData (playerData, petData);
		bf.Serialize (file, allData);
		file.Close ();
	}

	public void Load ()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			AllData allData = (AllData)bf.Deserialize (file);
			file.Close ();
			playerData = allData.playerData;
			petData = allData.petData;
		}
	}
	
	public bool IsLoggedIn ()
	{
		Load ();
		Debug.Log (playerData.Username);
		Debug.Log (playerData.AuthToken);
		return (!String.IsNullOrEmpty (playerData.AuthToken));
	}

	[Serializable]
	public class AllData
	{
		public PlayerData playerData;
		public PetData petData;

		public AllData (PlayerData playerData, PetData petData)
		{
			this.playerData = playerData;
			this.petData = petData;
		}
	}


	[Serializable]
	public class PlayerData
	{
		public string Username { get; set; }
		public string UserId { get; set; }
		public string AuthToken { get; set; }
		public float Money { get; set; }
	}

	[Serializable]
	public class PetData
	{
		public UInt32 PetId { get; set; }
		public string Petname { get; set; }
		public UInt64 Intelligence { get; set; }
		public UInt64 Strength { get; set; }
		public UInt64 Happiness { get; set; }
		public UInt64 Stamina { get; set; }
	}
}
