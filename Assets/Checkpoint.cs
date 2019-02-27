using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	
	public Vector2 direction;
	
	public bool reverse;
	
	private int checkpointNumber;
	private CheckpointManager checkpointManager;
	
	public void SetUp (CheckpointManager m, int n) {
		checkpointManager = m;
		checkpointNumber = n;
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			if (other.gameObject.GetComponent<PlayerMovement>().GetDirectionAsVector2() == direction)
			{
				checkpointManager.CheckpointHit(checkpointNumber);
				
				if (reverse)
				{
					other.gameObject.GetComponent<PlayerMovement>().reverseControls = !other.gameObject.GetComponent<PlayerMovement>().reverseControls;
				}
			}
		}
	}
}
