using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

	public Vector2 direction;
	
	private Checkpoint[] checkpoints;
	private bool[] checkpointsHit;
	
	// Use this for initialization
	void Start () {
		checkpoints = GetComponentsInChildren<Checkpoint>();
		checkpointsHit = new bool[checkpoints.Length];
		
		for (int i = 0; i < checkpoints.Length; i++)
		{
			checkpoints[i].SetUp(this, i);
			checkpointsHit[i] = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			if (other.gameObject.GetComponent<PlayerMovement>().GetDirectionAsVector2() == direction && AllCheckpointsHit())
			{
				// if (currentTime != 0)
				// {
					// // Debug.Log(currentTime.ToString("f2"));
					// lastTime = currentTime;
					// lastTimeText.text = lastTime.ToString("00.00");
					// if (lastTime < bestTime || bestTime == 0)
					// {
						// bestTime = lastTime;
						// bestTimeText.text = bestTime.ToString("00.00");
					// }
				// }
				
				// StopCoroutine("StartTimer");
				// StartCoroutine("StartTimer");
			}
		}
	}
	
	public void CheckpointHit (int n) {
		checkpointsHit[n] = true;
	}
	
	bool AllCheckpointsHit () {
		for (int i = 0; i < checkpointsHit.Length; i++)
		{
			if (checkpointsHit[i] == false)
			{
				return false;
			}
		}
		
		for (int i = 0; i < checkpointsHit.Length; i++)
		{
			checkpointsHit[i] = false;
		}
		
		// Debug.Log("T");
		return true;
	}
}
