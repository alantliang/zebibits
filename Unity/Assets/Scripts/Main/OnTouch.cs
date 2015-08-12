﻿using UnityEngine;
using System.Collections;

public class OnTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			Debug.Log("Touched here:" + touchDeltaPosition.ToString());
			// Move object across XY plane
		}
	}
}
