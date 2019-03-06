using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTail : MonoBehaviour {
	
	private TrailRenderer trailR;
	
	void Awake () {
		trailR = GetComponent<TrailRenderer>();
	}
	
	public void Clear () {
		trailR.Clear();
	}
}
