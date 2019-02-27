using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {
	
	private Timer timerScript;
	
	private Vector2 enterDir;
	private Vector2 exitDir;
	private Vector2 lastExitDir;
	
	private GameObject playerGO;
	private PlayerMovement playerMovementScript;
	
	// Use this for initialization
	void Start () {
		timerScript = GetComponent<Timer>();
		playerGO = GameObject.Find("Player");
		
		playerMovementScript = playerGO.GetComponent<PlayerMovement>();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject == playerGO)
		{
			enterDir = playerMovementScript.GetDirectionAsVector2();
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject == playerGO)
		{
			exitDir = playerMovementScript.GetDirectionAsVector2();
			
			// if lastExitDir is not set yet, this must be the start of the first lap, and so we can't end it
			if (lastExitDir != Vector2.zero)
			{		
				// if (exitDir == lastExitDir == enterDir)
				if ((exitDir == lastExitDir) && (exitDir == enterDir) && (lastExitDir == enterDir))
				{
					// if the direction the player exited last time, the current time and the direction they entered are all the same, it must be an honest lap
					// (unless they've glitched it lolololol)
					Lap();	
				}
			}else{
				// if lastExitDir hasn't been set yet the level must not have started, so start it!
				timerScript.StartLevel();
			}
			
			lastExitDir = playerMovementScript.GetDirectionAsVector2();
		}
	}
	
	void Lap () {
		timerScript.LapComplete();
	}
}
