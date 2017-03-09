using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	
	public Vector2 direction;
	
	public bool reverse;
	
	private int checkpointNumber;
	private Timer timer;
	
	public void SetUp (Timer t, int n) {
		timer = t;
		checkpointNumber = n;
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			if (other.gameObject.GetComponent<PlayerMovement>().GetDirectionAsVector2() == direction)
			{
				timer.CheckpointHit(checkpointNumber);
				
				if (reverse)
				{
					other.gameObject.GetComponent<PlayerMovement>().reverseControls = !other.gameObject.GetComponent<PlayerMovement>().reverseControls;
				}
			}
		}
	}
}
