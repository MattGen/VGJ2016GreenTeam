using UnityEngine;
using System.Collections;

public class Fireextinguisher : MonoBehaviour {

	private float elapsedTime;
	InterractableObject interObject = null;

	void Update(){
		if (interObject != null) {
			if (elapsedTime >= 2f) {
				elapsedTime = 0;
				interObject.ExtinctFire ();
			}
			else
				elapsedTime += Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider other){
		var interObj = other.GetComponent<InterractableObject> ();
		if (interObj != null && interObj.Fire) {
			elapsedTime = 0;
			interObject = interObj;
			Debug.Log ("Enter");
		}	
	}

	void OnTriggerExit(Collider other){
		var interObj = other.GetComponent<InterractableObject> ();
		if (interObj != null && interObj.Fire) {
			elapsedTime = 0;
			interObject = null;
			Debug.Log ("Exit");
		}	
	}


}
