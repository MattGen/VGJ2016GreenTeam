using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Indicators : MonoBehaviour
{
    public InterractableObject Valve;
    public Image ValveIndicator;

	public InterractableObject Fire;
	public Image FireIndicator;

	
	// Update is called once per frame
	void Update ()
	{
		var ratio = 1 - Valve.currentRotationTime / Valve.RotationTime;
	    ValveIndicator.fillAmount = ratio/2;
        ValveIndicator.color = new Color(1-ratio, ratio, 0);
			
		ratio = 1 - Fire.currentRotationTime / Fire.RotationTime;
		FireIndicator.fillAmount = ratio/2;
		FireIndicator.color = new Color(1-ratio, ratio, 0);
	}
}
