using UnityEngine;
using UnityEngine.UI;
// using System;
using System.Collections;

public class Timer : MonoBehaviour {
	
	public Text currentTimeText;
	public Text lastTimeText;
	public Text bestTimeText;
	
	public Vector2 direction;
	
	private float currentTime;
	private float lastTime;
	private float bestTime;
	
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
	
	public void StartLevel () {
		StartCoroutine("StartTimer");
	}
	
	IEnumerator StartTimer () {
		currentTime = 0;
		
		while (true)
		{
			currentTime += Time.deltaTime;
			// currentTime++;
			currentTimeText.text = currentTime.ToString("00.0");
			
			if (currentTime >= 99.9f)
			{
				currentTimeText.text = "99.9";
			}
			
			yield return null;
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
	
	public void LapComplete () {
		if (currentTime != 0)
		{
			// Debug.Log(currentTime.ToString("f2"));
			lastTime = currentTime;
			lastTimeText.text = lastTime.ToString("00.00");
			if (lastTime < bestTime || bestTime == 0)
			{
				bestTime = lastTime;
				bestTimeText.text = bestTime.ToString("00.00");
			}
		}
		
		StopCoroutine("StartTimer");
		StartCoroutine("StartTimer");
	}
}
