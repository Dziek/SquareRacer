using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour {
	
	private LineRenderer[] lineRs;
	
	void Awake () {
		lineRs = GetComponentsInChildren<LineRenderer>();
		
		foreach (LineRenderer lR in lineRs)
		{
			lR.SetPosition(0, transform.position);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		foreach (LineRenderer lR in lineRs)
		{
			// lR.transform.Translate();
			
			// lR.transform.position = new Vector3(lR.transform.position.x + Mathf.PingPong(Time.time, 3), lR.transform.position.y, lR.transform.position.z);
			
			lR.SetPosition(1, lR.transform.position);
		}
	}
}
