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
        var ratio = Valve.RotationTime / Valve.currentRotationTime;
	    ValveIndicator.fillAmount = ratio;
        ValveIndicator.color = new Color(1-ratio, ratio, 0);
	}
}
