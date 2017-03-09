using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 10; i++)
		{
			if (Input.GetKeyDown("" + i))
			{
				Application.LoadLevel(i);
			}
		}
	}
}
