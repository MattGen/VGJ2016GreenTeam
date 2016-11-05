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

	public void Start() {
		currentRotationTime = 0;
	}

	public void Update(){
		if (Activated && currentRotationTime < RotationTime) {			
			currentRotationTime += Time.deltaTime/10;
			transform.Rotate(new Vector3(0,0,-0.1f));
		}

		if (Fire && Activated) {
			var particle = transform.GetComponent<ParticleSystem> ();
			if(!particle.isPlaying)
				particle.Play ();
		} else if (Fire && !Activated) {
			var particle = transform.GetComponent<ParticleSystem> ();
			particle.Stop ();
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
		currentRotationTime -= Time.deltaTime;
		return true;
	}

	public void ExtinctFire(){
		gameObject.SetActive (false);
		Scheduler.Get ().ActivateNextFire (this);
	}
}
