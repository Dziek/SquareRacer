using UnityEngine;
using UnityEngine.UI;
// using System;
using System.Collections;

public class Timer : MonoBehaviour {
	
	public Text currentTimeText;
	public Text lastTimeText;
	public Text bestTimeText;
	
	private float currentTime;
	private float lastTime;
	private float bestTime;
	
	private AudioController audioController;
	
	void Awake () {
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
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
		
		StopCoroutine("StartTimer");
		StartCoroutine("StartTimer");
	}
}
