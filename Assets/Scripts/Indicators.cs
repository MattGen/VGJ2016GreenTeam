using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Indicators : MonoBehaviour
{
    public InterractableObject Valve;
    public Image ValveIndicator;

	
	// Update is called once per frame
	void Update ()
	{
		var ratio = 1 - Valve.currentRotationTime / Valve.RotationTime;
		Debug.Log (ratio);
	    ValveIndicator.fillAmount = ratio/2;
        ValveIndicator.color = new Color(1-ratio, ratio, 0);
	}
}
