using UnityEngine;
using System.Collections;

public class LerpColor : MonoBehaviour {
	
	public float speed = 1;
	
	public float startingDuration = 10;
	[Tooltip("-numbers are random")]
	[Range(-0.1f, 1)]
	public float startingPoint = 0;
	public Gradient gradient;
	
	private float duration;
	private Color currentColor;
	
	private SpriteRenderer sR;
	private Camera cR;
	
	void Awake () {
		if (GetComponent<SpriteRenderer>())
		{
			sR = GetComponent<SpriteRenderer>();
		}
		if (GetComponent<Camera>())
		{
			cR = GetComponent<Camera>();
		}
	}
	
	// Use this for initialization
	void Start () {
		if (startingPoint < 0)
		{
			startingPoint = Random.Range(0, 1f);
		}
		
		duration = startingDuration;
		
		StartCoroutine("Lerp");
	}
	
	// Update is called once per frame
	// void Update () {
		// duration = ColorController.duration;
		// speed = ColorController.speed;
		
		// t += (Time.deltaTime * speed);
		
		// float t = startingPoint + Mathf.Repeat(Time.time, duration) / duration;
		// if (t > 1)
		// {
			// t -= 1;
		// }
		// currentColor = gradient.Evaluate(t);
		
		// UpdateObject();
	// }
	
	IEnumerator Lerp () {
		
		float l = 0;
		
		while (true)
		{
			// duration = ColorController.duration;
			speed = ColorController.speed;
			
			l += (Time.deltaTime * speed);
			
			// float t = startingPoint + Mathf.Repeat(Time.time, duration) / duration;
			float t = startingPoint + Mathf.Repeat(l, duration) / duration;
			// float t = startingPoint + Mathf.Lerp(0, duration, Time.time);
			if (t > 1)
			{
				t -= 1;
			}
			currentColor = gradient.Evaluate(t);
			
			UpdateObject();
			
			yield return null;
		}
	}
	
	void UpdateObject () {
		if (sR != null)
		{
			sR.color = currentColor;
		}
		if (cR != null)
		{
			cR.backgroundColor = currentColor;
		}
	}
	
	// public void
}
