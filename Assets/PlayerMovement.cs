using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float startingSpeed = 4;
	public float maxSpeed = 40;
	public float speedIncrease = 0.5f;
	
	private float speed;
	
	public bool reverseControls;
	
	public enum directions{
		Up,
		Down,
		Right,
		Left,
		None
	} [HideInInspector] public directions direction = directions.None, selectedDirection;
	
	private Rigidbody2D rb2D;
	
	// Use this for initialization
	void Start () {
		// sR = GetComponent<SpriteRenderer>();	
		// startingPos = transform.position;
		
		// Reset();
		rb2D = GetComponent<Rigidbody2D>();
		speed = startingSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		// if (GameStates.GetState() == "Playing")
		{
			CheckControls();
			// Move();
		}
	}
	
	void FixedUpdate () {
		rb2D.MovePosition(rb2D.position + (Vector2)GetDirectionAsVector() * (speed * Time.fixedDeltaTime));
	}
	
	void CheckControls () {
		if (reverseControls == false)
		{
			if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
			{
				RegisterInput("Up");
			}
			if (Input.GetKeyDown("s") || Input.GetKeyDown("down")) 
			{
				RegisterInput("Down");
			}
			if (Input.GetKeyDown("d") || Input.GetKeyDown("right")) 
			{	
				RegisterInput("Right");
			}
			if (Input.GetKeyDown("a") || Input.GetKeyDown("left")) 
			{
				RegisterInput("Left");
			}
		}else{
			if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
			{
				RegisterInput("Down");
			}
			if (Input.GetKeyDown("s") || Input.GetKeyDown("down")) 
			{
				RegisterInput("Up");
			}
			if (Input.GetKeyDown("d") || Input.GetKeyDown("right")) 
			{	
				RegisterInput("Left");
			}
			if (Input.GetKeyDown("a") || Input.GetKeyDown("left")) 
			{
				RegisterInput("Right");
			}
		}
		
		
		// if (Input.GetKeyDown("space")) 
		// {
			// ShootProjectile();
		// }
	}
	
	public void RegisterInput (string dir) {
		
		selectedDirection = (directions) System.Enum.Parse (typeof(directions), dir);
		
		if (direction != selectedDirection)
		{
			if (GetDirectionAsVector2() != -GetDirectionAsVector2(selectedDirection))
			{
				speed += speedIncrease;
				// ColorController.DecreaseDuration(0.25f);
				ColorController.IncreaseSpeed(0.25f);
			}else{
				SlowDown();
			}
		}
		
		// if (direction == selectedDirection && boosting == false && boostReady)
		// {
			// boosting = true;
			// boostReady = false;
			
			// StartCoroutine("Boost");
		// }else{
			direction = selectedDirection;
		// }
		
		// if (lvlInfoDisplay != null && canGoAway)
		// {
			// lvlInfoDisplay.GetComponent<LevelInfoDisplay>().GoAway();
			// canGoAway = false;
		// }
		
		// if (controlledObjectScript != null)
		// {
			// controlledObjectScript.UpdateDir(direction.ToString());
		// }
	}
	
	void Move () {
		// switch (direction)
		// {
			// case directions.Up:
				// transform.Translate(Vector3.up * (Time.deltaTime * speedL), Space.World);
			// break;
			// case directions.Down:
				// transform.Translate(Vector3.up * (-Time.deltaTime * speedL), Space.World);
			// break;
			// case directions.Right:
				// transform.Translate(Vector3.right * (Time.deltaTime * speedL), Space.World);
			// break;
			// case directions.Left:
				// transform.Translate(Vector3.right * (-Time.deltaTime * speedL), Space.World);
			// break;
		// }
		
		Vector3 dir = GetDirectionAsVector();
		
		// if (reverseControls)
		// {
			// dir = -dir;
		// }
		
		transform.Translate(dir * (Time.deltaTime * speed), Space.World);
		// transform.Translate(GetDirectionAsVector() * (Time.deltaTime * speed), Space.World);
	}
	
	public Vector3 GetDirectionAsVector () {
		switch (direction)
		{
			case directions.Up:
				return Vector3.up;
			
			case directions.Down:
				return Vector3.down;
			
			case directions.Right:
				return Vector3.right;
			
			case directions.Left:
				return Vector3.left;
			
		}
		
		return Vector3.zero;
	}
	
	public Vector2 GetDirectionAsVector2 () {
		switch (direction)
		{
			case directions.Up:
				return Vector2.up;
			
			case directions.Down:
				return Vector2.down;
			
			case directions.Right:
				return Vector2.right;
			
			case directions.Left:
				return Vector2.left;
			
		}
		
		return Vector3.zero;
	}
	
	public Vector2 GetDirectionAsVector2 (directions d) {
		switch (d)
		{
			case directions.Up:
				return Vector2.up;
			
			case directions.Down:
				return Vector2.down;
			
			case directions.Right:
				return Vector2.right;
			
			case directions.Left:
				return Vector2.left;
			
		}
		
		return Vector3.zero;
	}
	
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Wall" && other.contacts[0].normal == -(Vector2)GetDirectionAsVector())
		{
			SlowDown();
			// Debug.Log("HIT");
		}
	}
	
	void SlowDown () {
		speed = startingSpeed;
		// ColorController.ResetDuration();
		ColorController.ResetSpeed();
		
		// Debug.Log("S");
	}
}
