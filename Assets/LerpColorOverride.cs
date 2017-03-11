using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpColorOverride : MonoBehaviour {

	public float speed = 1;
	
	public float startingDuration = 10;
	// [Tooltip("-numbers are random")]
	// [Range(-0.1f, 1)]
	// public float startingPoint = 0;
	public Gradient gradient;
	
	void Start () {
		Messenger<float, float, Gradient>.Broadcast("lerpcolor", speed, startingDuration, gradient);
	}
}
