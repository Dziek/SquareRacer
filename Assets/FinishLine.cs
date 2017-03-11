using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {
	
	public Vector2 setDirection;
	
	private Timer timerScript;
	
	private Vector2 crossedDirection;
	
	// Use this for initialization
	void Start () {
		timerScript = GetComponent<Timer>();
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			if (crossedDirection == Vector2.zero)
			{
				timerScript.StartLevel();
			}
			
			Vector2 currentDir = other.gameObject.GetComponent<PlayerMovement>().GetDirectionAsVector2();
			
			if (setDirection == Vector2.zero)
			{
				if (currentDir == crossedDirection)
				{
					Lap();
				}
				
			}else{
				if (currentDir == crossedDirection && currentDir == setDirection)
				{
					Lap();
				}
			}
			
			crossedDirection = currentDir;
		}
	}
	
	void Lap () {
		timerScript.LapComplete();
	}
}
