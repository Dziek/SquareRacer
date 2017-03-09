using UnityEngine;
using System.Collections;

public class ColorController : MonoBehaviour {
	
	public static float startingDuration = 10;
	public static float duration = 10;
	public static float startingSpeed = 1;
	public static float speed = 1;
	
	public static void DecreaseDuration (float i) {
		duration -= i;
	}
	
	public static void ResetDuration () {
		duration = startingDuration;
	}	
	
	public static void IncreaseSpeed (float i) {
		speed += i;
	}
	
	public static void ResetSpeed () {
		speed = startingSpeed;
	}	
}
