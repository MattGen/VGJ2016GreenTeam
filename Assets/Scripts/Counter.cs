using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

	public int targetTime;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float restant = (targetTime - Time.time);
		if (restant > 0) {
			GetComponent<Text> ().text = ((int)restant).ToString ();
		} else {
			GetComponent<Text> ().text = "Gagné !";
		}
			

	}
}
