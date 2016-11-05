using UnityEngine;
using System.Collections;

public class InterractableObject : MonoBehaviour {

	public float RotationTime;
	private float currentRotationTime;
	private float timeSinceLastRotation;

	public void Start() {
		currentRotationTime = RotationTime;
	}

	public void Update(){
		if (currentRotationTime < RotationTime) {			
			currentRotationTime += Time.deltaTime/10;
			transform.Rotate(new Vector3(0,0,-0.1f));
		}
	}

	public bool Rotate(){
		if (currentRotationTime <= 0.2f) {
			return false;
		}
		currentRotationTime -= Time.deltaTime;
		return true;
	}
}
