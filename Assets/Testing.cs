﻿using UnityEngine;
using System.Collections;

public class Testing : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("r"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
