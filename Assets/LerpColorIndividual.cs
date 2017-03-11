using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpColorIndividual : LerpColor {

	public override void GetRenderers () {
		
		base.GetRenderers();
		
		if (GetComponent<SpriteRenderer>())
		{
			sR = GetComponent<SpriteRenderer>();
		}
		if (GetComponent<TrailRenderer>())
		{
			tR = GetComponent<TrailRenderer>();
		}
		if (GetComponent<Text>())
		{
			text = GetComponent<Text>();
		}
	}
	
	public override void UpdateObject () {
		if (sR != null)
		{
			sR.color = currentColor;
		}
		// if (tR != null)
		// {
			// tR.color = currentColor;
		// }
		if (text != null)
		{
			text.color = currentColor;
		}
		
		if (cR != null)
		{
			cR.backgroundColor = currentColor;
		}
	}
}
