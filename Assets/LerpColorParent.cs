using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

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
		if (GetComponentsInChildren<TextMeshProUGUI>().Length > 0)
		{
			textPro_array = GetComponentsInChildren<TextMeshProUGUI>();
		}
	}
	
	// public override void UpdateObject () {
		// base.UpdateObject();
	// }
}
