using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LerpColorParent : LerpColor {
	
	public override void GetRenderers () {
		
		base.GetRenderers();
		
		if (GetComponentsInChildren<SpriteRenderer>().Length > 0)
		{
			sR_array = GetComponentsInChildren<SpriteRenderer>();
		}
		if (GetComponentsInChildren<TrailRenderer>().Length > 0)
		{
			tR_array = GetComponentsInChildren<TrailRenderer>();
		}
		if (GetComponentsInChildren<Text>().Length > 0)
		{
			text_array = GetComponentsInChildren<Text>();
		}
	}
	
	// public override void UpdateObject () {
		// base.UpdateObject();
	// }
}
