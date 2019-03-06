using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphics : MonoBehaviour {
	
	public Sprite moveSprite;
	private Sprite stillSprite;
	
	private SpriteRenderer sR;
	
	private PlayerMovement playerMovement;
	
	void Awake () {
		playerMovement = GetComponentInParent<PlayerMovement>();
		sR = GetComponent<SpriteRenderer>();
		
		stillSprite = sR.sprite;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Switch (playerMovement.GetDirectionAsVector())
		// {
			// case Vector2.right:
				// sR.sprite 
			// break;
			
			// case default:
				// sR.sprite = stillSprite;
			// break;
		// }
	}
	
	public void FaceDirection (Vector2 newDir) {
		// transform.eulerAngles = newDir;
		transform.rotation = Quaternion.LookRotation(newDir);
		
		// Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		// Vector2 dir = (Vector2)Input.mousePosition - objectPos; 
		float playerRotationAngle = Mathf.Atan2 (newDir.y,newDir.x) * Mathf.Rad2Deg; 
		transform.rotation = Quaternion.Euler (new Vector3(0,0,playerRotationAngle - 90));
	}
	
	public void MoveSprite () {
		sR.sprite = moveSprite;
	}
	
	public void StillSprite () {
		sR.sprite = stillSprite;
	}
}
