using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	private TrailRenderer tR;
	private Camera cR;
	private Text text;
	
	void Awake () {
		if (GetComponent<SpriteRenderer>())
		{
			sR = GetComponent<SpriteRenderer>();
		}
		if (GetComponent<TrailRenderer>())
		{
			tR = GetComponent<TrailRenderer>();
		}
		if (GetComponent<Camera>())
		{
			cR = GetComponent<Camera>();
		}
		if (GetComponent<Text>())
		{
			text = GetComponent<Text>();
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
			
			t += ColorController.skip;
			
			// 0.7 += 0.5
			// 1.2
			// - 1.5
			
			if (t > 1)
			{
				// t -= 1;
				t -= Mathf.Clamp(Mathf.Floor(t), 0, 10000000000000000000);
				// t -= (1 + ColorController.skip);
			}
			// Debug.Log(t);
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
		if (tR != null)
		{
			// tR.color = currentColor;
		}
		if (cR != null)
		{
			cR.backgroundColor = currentColor;
		}
		if (text != null)
		{
			text.color = currentColor;
		}
	}
	
	// public void
}
