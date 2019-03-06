using UnityEngine;
using UnityEngine.UI;
// using System;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour {
	
	// public Text lastTimeText;
	// public Text bestTimeText;
	// public Text currentTimeText;
	
	public TextMeshProUGUI lastTimeText;
	public TextMeshProUGUI bestTimeText;
	public TextMeshProUGUI currentTimeText;
	
	private float currentTime;
	private float lastTime;
	private float bestTime;
	
	private bool pauseTimer;
	
	private AudioController audioController;
	private PlayerMovement playerMovement;
	
	void Awake () {
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
	}
	
	public void StartLevel () {
		StartCoroutine("StartTimer");
	}
	
	public void PauseTimer () {
		pauseTimer = true;
	}
	
	public void UnpauseTimer () {
		pauseTimer = false;
	}
	
	IEnumerator StartTimer () {
		currentTime = 0;
		
		while (true)
		{
			if (pauseTimer == false)
			{
				currentTime += Time.deltaTime;
				// currentTime++;
				currentTimeText.text = currentTime.ToString("00.0");
				
				if (currentTime >= 99.9f)
				{
					currentTimeText.text = "Na.N";
				}
			}
			
			yield return null;
		}
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
				
				audioController.NewRecord();
			}else{
				audioController.LapComplete();
			}
		}
		
		// ColorController.SkipForward();
		playerMovement.LapComplete();
		
		StopCoroutine("StartTimer");
		StartCoroutine("StartTimer");
	}
}
