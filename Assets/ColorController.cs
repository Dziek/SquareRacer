using UnityEngine;
using System.Collections;

public class ColorController : MonoBehaviour {
	
	// public static float startingDuration = 10;
	// public static float duration = 10;
	public static float startingSpeed = 1; // this doesn't set starting speed what does?
	public static float stopSpeed = 0.2f;
	public static float minSpeed = 1f;
	public static float maxSpeed = 10f;
	public static float speed = 1;
	
	public static float skip = 0;
	
	// public static void DecreaseDuration (float i) {
		// duration -= i;
	// }
	
	// public static void ResetDuration () {
		// duration = startingDuration;
	// }

	public static void UpdateSpeed (float percentage) {
		if (percentage == 0)
		{
			speed = stopSpeed;
		}else{
			speed = Mathf.Lerp(minSpeed, maxSpeed, percentage);
		}
		
	}
	
	public static void IncreaseSpeed (float i) {
		speed += i;
	}
	
	public static void ResetSpeed () {
		speed = 0;
	}	
	
	public static void SkipForward () {
		skip += 0.5f;
	}	
}
