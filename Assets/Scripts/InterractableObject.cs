using UnityEngine;
using System.Collections;

public class InterractableObject : MonoBehaviour {

	public bool Fire;

	public float RotationTime;
    [HideInInspector]
	public float currentRotationTime;
	[HideInInspector]
	public bool Activated;

	private float timeSinceLastRotation;

	private Vector3 centerPoint;

	public void Start() {
		currentRotationTime = 0;
		if(Fire)
			gameObject.SetActive (false);

		var pos = transform.Find ("ToolPosition");
		if(pos != null)
			centerPoint = pos.transform.position;
	}

	public void Update(){
		if (Activated && currentRotationTime < RotationTime) {			
			currentRotationTime += Time.deltaTime/10;
			if(!Fire)
				transform.Rotate(new Vector3(-0.1f,0,0));
		}
	}

	public bool Rotate(){
		if (!Activated) {
			return false;
		}
		if (currentRotationTime < 0f) {
			Activated = false;
			Scheduler.Get ().ActivateNextValve (this);
			return false;
		}
		var audio = GetComponent<AudioSource> ();
		if (!audio.isPlaying)
			audio.Play ();
		currentRotationTime -= Time.deltaTime;
		transform.Rotate(new Vector3(-1f,0,0));
		return true;
	}

	public void ExtinctFire(){
		Activated = false;
		gameObject.SetActive (false);
		Scheduler.Get ().ActivateNextFire (this);
	}
}
