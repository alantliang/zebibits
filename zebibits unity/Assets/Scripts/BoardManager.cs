using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

public class BoardManager : MonoBehaviour
{	
	public int columns = 100;
	public int rows = 100;
	public GameObject[] floorTiles;									//Array of floor prefabs.
	private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
	
	void GrassSetup ()
	{
		// Setup grass tiles in order i.e. grassTile1, grassTile2, etc. with alternating rows
		boardHolder = new GameObject ("Board").transform;
		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {
				// we want checkerboard pattern of grass tiles
				GameObject toInstantiate;
				
				if (x % 2 == 0) {
					if (y % 2 == 0) {
						toInstantiate = floorTiles [0];
					} else {
						toInstantiate = floorTiles [1];
					}
				} else {
					if (y % 2 == 0) {
						toInstantiate = floorTiles [1];
					} else {
						toInstantiate = floorTiles [0];
					}
				}
				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);
			}
		}
	}
		
		
	public void SetupScene ()
	{
		GrassSetup ();
	}
}
