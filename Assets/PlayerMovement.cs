﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float startingSpeed = 4;
	public float maxSpeed = 40;
	public float speedIncrease = 0.5f;
	public float speedIncreaseMin = 0.05f;
	
	private float speed;
	private float speed_TEST;
	
	public bool reverseControls;
	
	public enum directions{
		Up,
		Down,
		Right,
		Left,
		None
	} [HideInInspector] public directions direction = directions.None, selectedDirection;
	
	private Rigidbody2D rb2D;
	private TrailRenderer tR;
	
	private CameraShake cameraShakeScript;
	private AudioController audioController;
	
	// private bool hit;
	
	void Awake () {
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
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
		
		CheckControls();
		
		if (speed >= startingSpeed * 2)
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
		
		if (direction != selectedDirection)
		{
			if (GetDirectionAsVector2() != -GetDirectionAsVector2(selectedDirection))
			{
				if (direction != directions.None)
				{
					speed_TEST += speedIncrease;
					
					
					// speed += speedIncrease;
					// speed += speedIncrease * (1 - GetSpeedPercentage());
					speed += Mathf.Clamp(speedIncrease * (1.2f - GetSpeedPercentage()), speedIncreaseMin, speedIncrease); 
					// UNSURE (0.05 is too shallow. Trying 0.3f speedIncreaseMin. I like there being a curve, but can't be too shallow
					Debug.Log("Current Speed is: " + speed + " instead of: " + speed_TEST);
					
					ColorController.IncreaseSpeed(0.1f);
					
					audioController.PlayerTurn();
				}else{
					audioController.StartLevelAudio();
				}
			}else{
				SlowDown();
			}
		}
		
			direction = selectedDirection;
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
			// hit = true;
			
			Messenger<float>.Broadcast("screenshake", speed/50);
			
			audioController.Crash(speed);
			
			SlowDown();
			// Debug.Log("HIT");
		}
		// else{
			// hit = false;
		// }
	}
	
	void SlowDown () {
		
		Debug.Log("PlayerSpeed was: " + speed);
		
		
		
		speed_TEST = startingSpeed;
		speed = startingSpeed;
		// speed = Mathf.Clamp(speed - speedIncrease, startingSpeed, maxSpeed);
		
		// ColorController.ResetDuration();
		ColorController.ResetSpeed();
		
		if (tR != null)
		{
			tR.Clear();
		}
		
		direction = directions.None;
		
		// Debug.Log("S");
	}
	
	public float GetSpeedPercentage () {
		return Mathf.InverseLerp(startingSpeed, maxSpeed, speed);
	}
}
