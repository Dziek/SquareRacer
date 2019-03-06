using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float startingSpeed = 4;
	public float maxSpeed = 40;
	public float speedIncrease = 0.5f;
	public float speedIncreaseMin = 0.05f;
	
	public float speedIncreaseTurn = 0.1f;
	public float speedIncreaseLap = 2;
	public float speedDecreaseBump = 0.5f;
	
	private float speed;
	private float speed_TEST;
	
	public bool reverseControls;
	public bool blockControls;
	
	public enum directions{
		Up,
		Down,
		Right,
		Left,
		None
	} [HideInInspector] public directions direction, crashDirection, selectedDirection;
	
	private Rigidbody2D rb2D;
	private TrailRenderer tR; // HAHA TODO decide if I want this of PlayerTail
	
	private CameraShake cameraShakeScript;
	private AudioController audioController;
	private Timer timerScript;
	private PlayerGraphics playerGraphics;
	private PlayerTail playerTail;
	
	// private bool hit;
	
	void Awake () {
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		timerScript = GameObject.Find("StartingLine").GetComponent<Timer>();
		playerGraphics = GetComponentInChildren<PlayerGraphics>();
		playerTail = GetComponentInChildren<PlayerTail>();
	}
	
	// Use this for initialization
	void Start () {
		// sR = GetComponent<SpriteRenderer>();	
		// startingPos = transform.position;
		
		// Reset();
		rb2D = GetComponent<Rigidbody2D>();
		
		if (GetComponent<TrailRenderer>())
		{
			tR = GetComponent<TrailRenderer>();
		}
		
		cameraShakeScript = Camera.main.GetComponent<CameraShake>();
		
		speed = startingSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (blockControls == false)
		{
			CheckControls();
		}
		
		if (speed >= startingSpeed * 1.5f)
		{
			// Messenger<float>.Broadcast("screenshake", speed/150);
			cameraShakeScript.ConstantShake(speed/1000);
		}
	}
	
	void FixedUpdate () {
		rb2D.MovePosition(rb2D.position + (Vector2)GetDirectionAsVector() * (speed * Time.fixedDeltaTime));
	}
	
	void CheckControls () {
		
		if (reverseControls == false)
		{
			if (Input.GetAxisRaw("Vertical") > 0.5f)
			{
				RegisterInput("Up");
			}
			else
			if (Input.GetAxisRaw("Vertical") < -0.5f) 
			{
				RegisterInput("Down");
			}
			else
			if (Input.GetAxisRaw("Horizontal") > 0.5f) 
			{	
				RegisterInput("Right");
			}
			else
			if (Input.GetAxisRaw("Horizontal") < -0.5f) 
			{
				RegisterInput("Left");
			}
		}else{
			if (Input.GetAxisRaw("Vertical") > 0.5f)
			{
				RegisterInput("Down");
			}
			else
			if (Input.GetAxisRaw("Vertical") < -0.5f) 
			{
				RegisterInput("Up");
			}
			else
			if (Input.GetAxisRaw("Horizontal") > 0.5f) 
			{	
				RegisterInput("Left");
			}
			else
			if (Input.GetAxisRaw("Horizontal") < -0.5f) 
			{
				RegisterInput("Right");
			}
		}
	}
	
	public void RegisterInput (string dir) {
		
		selectedDirection = (directions) System.Enum.Parse (typeof(directions), dir);
		
		if (selectedDirection == crashDirection && crashDirection != directions.None) // this means the player is trying to go into a wall. This is bad
		{
			return;
		}
		
		if (direction != selectedDirection)
		{
			if (GetDirectionAsVector2() != -GetDirectionAsVector2(selectedDirection))
			{
				if (direction != directions.None)
				{
					speed_TEST += speedIncrease;
					
					// NORMAL WAY OF DOING IT
					// speed += speedIncrease; // normal
					// speed += speedIncrease * (1 - GetSpeedPercentage()); // decreases speedIncrease
					// speed += Mathf.Clamp(speedIncrease * (1.2f - GetSpeedPercentage()), speedIncreaseMin, speedIncrease); // levels out speedIncrease			
					// UNSURE (0.05 is too shallow. Trying 0.3f speedIncreaseMin. I like there being a curve, but can't be too shallow
					// Debug.Log("Current Speed is: " + speed + " instead of: " + speed_TEST);
					
					speed += speedIncreaseTurn;
					// Debug.Log("Current Speed is: " + speed + " instead of: " + speed_TEST);
					
					// ColorController.IncreaseSpeed(0.02f);
					ColorController.UpdateSpeed(GetSpeedPercentage());
					
					audioController.PlayerTurn();
				}else{
					audioController.StartLevelAudio();
					playerGraphics.MoveSprite();
					timerScript.UnpauseTimer();
				}
			}else{
				SlowDown();
			}
		}
		
		direction = selectedDirection;
		crashDirection = directions.None;
		playerGraphics.FaceDirection(GetDirectionAsVector2());
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
		if (other.gameObject.tag == "Wall")
		{	
			if (other.contacts[0].normal == -(Vector2)GetDirectionAsVector())
			{
				Messenger<float>.Broadcast("screenshake", speed/50);
				
				audioController.Crash(speed);
				playerGraphics.StillSprite();
				
				StartCoroutine("BlockControls");
				
				SlowDown();
				Debug.Log("Crash");
			}else{
				Messenger<float>.Broadcast("screenshake", speed/100);
				speed -= speedDecreaseBump;
				ColorController.UpdateSpeed(GetSpeedPercentage());
				
				playerTail.Clear();
				Debug.Log("Close call");
			}
		}
	}
	
	void OnCollisionStay2D (Collision2D other) {
		// I need the normal stuff for Stay
		if (other.gameObject.tag == "Wall" && other.contacts[0].normal == -(Vector2)GetDirectionAsVector())
		{
			audioController.Crash(speed);
			playerGraphics.StillSprite();
			
			StartCoroutine("BlockControls");
			
			SlowDown();
			Debug.Log("HIT");
		}
	}
	
	void SlowDown () {
		
		Debug.Log("PlayerSpeed was: " + speed);
		
		playerTail.Clear();
		
		speed_TEST = startingSpeed;
		speed = startingSpeed;
		// speed = Mathf.Clamp(speed - speedIncrease, startingSpeed, maxSpeed);
		
		// ColorController.ResetDuration();
		// ColorController.ResetSpeed();
		ColorController.UpdateSpeed(GetSpeedPercentage());
		
		if (tR != null)
		{
			tR.Clear();
		}
		
		crashDirection = direction;
		direction = directions.None;
		ColorController.SkipForward();
		timerScript.PauseTimer();
		
		// Debug.Log("S");
	}
	
	public float GetSpeedPercentage () {
		return Mathf.InverseLerp(startingSpeed, maxSpeed, speed);
	}
	
	// this will possibly be used to get a buffer. For example, the first lap should really see much change, so maybe the "percentage" takes place from
	// startingSpeed +1 or something
	public float GetAdjustedSpeedPercentage () {
		float bufferValue = speedIncreaseLap; // this was 2 when originally made. It should mean the first lap doesn't change much
		return Mathf.InverseLerp(startingSpeed + bufferValue, maxSpeed, speed);
	}
	
	IEnumerator BlockControls () {
		float t = 0;
		float blockDuration = 0.2f;
		
		// while (t < blockDuration)
		// {
			// t += Time.deltaTime;
			// return yield null;
		// }
		
		blockControls = true;
		
		yield return new WaitForSeconds(blockDuration);
		
		blockControls = false;
		
		
	}
	
	public void LapComplete () {
		// Debug.Break();
		speed += speedIncreaseLap;
		ColorController.UpdateSpeed(GetSpeedPercentage());
		// ColorController.IncreasedSpeed(0.2f);
		// ColorController.SkipForward();
	}
}
