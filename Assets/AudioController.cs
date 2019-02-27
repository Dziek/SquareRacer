using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	
	[Header("Clips")]
	public AudioClip lapCompleteNoise;
	public AudioClip newBestScoreNoise;
	
	public AudioClip crashNoise;
	public AudioClip turnNoise;
	
	[Header("Sources")]
	public AudioSource levelAS;
	public AudioSource levelAS2;
	public AudioSource playerAS;
	public AudioSource timerAS;
	
	private AudioSource audioSource;
	private PlayerMovement playerMovement;
	
	void Awake () {
		audioSource = GetComponent<AudioSource>();
		playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
	}
	
	void Update () {
		
		UpdateLevelTrackPitch();
		UpdateLevelTrackBassVolume();
	}
	
	void UpdateLevelTrackPitch () {
		float minPitch = 1;
		float maxPitch = 2;
		
		float pitch = Mathf.Lerp(minPitch, maxPitch, playerMovement.GetSpeedPercentage());
		
		levelAS.pitch = pitch;
		levelAS2.pitch = pitch;
	}
	
	void UpdateLevelTrackBassVolume () {
		float min = -0.1f;
		float max = 1;
		
		float v = Mathf.Lerp(min, max, playerMovement.GetSpeedPercentage());
		
		levelAS2.volume = v;
	}
	
	public void PlayClip (AudioClip clip, float pitch = 1) {
		audioSource.pitch = pitch;
		audioSource.PlayOneShot(clip);
	}
	
	public void PlayClip (AudioClip clip, AudioSource source, float pitch = 1) {
		source.pitch = pitch;
		source.PlayOneShot(clip);
	}
	
	public void NewRecord () {
		float minPitch = 1;
		float maxPitch = 2;
		
		float pitch = Mathf.Lerp(minPitch, maxPitch, playerMovement.GetSpeedPercentage());
		
		PlayClip(newBestScoreNoise, timerAS, pitch);
		Debug.Log(pitch);
	}
	
	public void LapComplete () {
		float minPitch = 1;
		float maxPitch = 2;
		
		float pitch = Mathf.Lerp(minPitch, maxPitch, playerMovement.GetSpeedPercentage());
		
		PlayClip(lapCompleteNoise, timerAS, pitch);
		Debug.Log(pitch);
	}
	
	public void Crash (float speed) {
		PlayClip(crashNoise, playerAS);
		
		StartCoroutine("FadeOutFadeIn");
	}
	
	public void PlayerTurn () {
		playerAS.pitch = Random.Range(1.0f, 2.0f);
		playerAS.PlayOneShot(turnNoise, 0.2f);
	}
	
	IEnumerator FadeOutFadeIn () {
		// float outTime = 0.219f;
		// float pause = 0.5f;
		float inTime = 1f;
		
		float outTime = Mathf.Lerp(0, 0.219f, playerMovement.GetSpeedPercentage());
		float pause = Mathf.Lerp(0, 2f, playerMovement.GetSpeedPercentage());
		// float inTime = Mathf.Lerp(1f, 2f, playerMovement.GetSpeedPercentage());
		
		float t = 0;
		
		while (t < outTime)
		{
			t += Time.deltaTime;
			levelAS.volume = 1 - Mathf.Lerp(0, outTime, t);
			yield return null;
		}
		
		levelAS.volume = 0;
		yield return new WaitForSeconds(pause);
		
		t = 0;
		
		while (t < inTime)
		{
			t += Time.deltaTime;
			levelAS.volume = Mathf.Lerp(0, inTime, t);
			yield return null;
		}
		
		levelAS.volume = 1;
	}
}

// https://freesound.org/people/nowherestudios/sounds/447842/
// https://www.zapsplat.com/sound-effect-category/drums-and-percussion/

// https://www.musicradar.com/news/tech/sampleradar-466-free-jazz-club-samples-291818
