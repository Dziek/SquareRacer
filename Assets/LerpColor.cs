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
	
	public bool ignoreOverride;
	
	private float duration;
	[HideInInspector] public Color currentColor;
	
	[HideInInspector] public SpriteRenderer[] sR_array;
	[HideInInspector] public TrailRenderer[] tR_array;
	[HideInInspector] public Text[] text_array;
	
	[HideInInspector] public SpriteRenderer sR;
	[HideInInspector] public TrailRenderer tR;
	[HideInInspector] public Text text;
	
	[HideInInspector] public Camera cR;
	
	void Awake () {
		Messenger<float, float, Gradient>.AddListener("lerpcolor", SetUp);
		
		GetRenderers();
	}
	
	void OnDestroy () {
		Messenger<float, float, Gradient>.RemoveListener("lerpcolor", SetUp);
	}
	
	public virtual void GetRenderers () {
		if (GetComponent<Camera>())
		{
			cR = GetComponent<Camera>();
		}
	}
	
	public void SetUp (float s, float d, Gradient g) {
		if (ignoreOverride == false)
		{
			speed = s;
			startingDuration = d;
			gradient = g;
		}
	}
	
	// Use this for initialization
	public void Start () {
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
	
	public IEnumerator Lerp () {
		
		float l = 0;
		
		while (true)
		{
			// duration = ColorController.duration;
			speed = ColorController.speed;
			
			l += (Time.deltaTime * speed);
			
			// float t = startingPoint + Mathf.Repeat(Time.time, duration) / duration;
			float t = startingPoint + Mathf.Repeat(l, duration) / duration;
			// float t = startingPoint + Mathf.Lerp(0, duration, Time.time);
			
			// t += ColorController.skip;
			
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
	
	public virtual void UpdateObject () {
		if (sR_array != null)
		{
			// sR.color = currentColor;
			foreach (SpriteRenderer c in sR_array)
			{
				c.color = currentColor;
			}
		}
		// if (tR != null)
		// {
			// tR.color = currentColor;
		// }
		if (text_array != null)
		{
			// text.color = currentColor;
			foreach (Text c in text_array)
			{
				c.color = currentColor;
			}
		}
		
		if (cR != null)
		{
			cR.backgroundColor = currentColor;
		}
	}
}
