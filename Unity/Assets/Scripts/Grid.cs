using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
	public GameObject plane;
	public int width = 10;
	public int height = 10;
	
	public GameObject[,] grid = new GameObject[10, 10];
	
	void Awake ()
	{
		grid = new GameObject[width, height];
		// our grid is in the x/y plane
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				GameObject gridPlane = (GameObject)Instantiate (plane);
				gridPlane.transform.position = new Vector3 (gridPlane.transform.position.x + x,
				gridPlane.transform.position.y + y, gridPlane.transform.position.z);
				grid [x, y] = gridPlane;
			}
		}
	}
	
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
