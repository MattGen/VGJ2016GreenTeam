using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scheduler : MonoBehaviour {

	public Indicators Indicator;
	public List<InterractableObject> Valves;
	public List<InterractableObject> Fires;

	public int Score = 0;

	private static Scheduler instance;
	void Awake(){
		instance = this;
	}

	public static Scheduler Get(){
		return instance;
	}

	void Start(){
		ActivateNextValve (null);
		StartCoroutine (WaitForFire());
	}

	private IEnumerator WaitForFire(){
		yield return new WaitForSeconds (30);
		ActivateNextFire (null);
	}

	public void ActivateNextValve(InterractableObject current){
		InterractableObject valve = null;
		do {
			valve = Valves [Random.Range (0, Valves.Count)];
		} while(valve == current);
		valve.Activated = true;
		Indicator.Valve = valve;
	}
		
	public void ActivateNextFire(InterractableObject current){
		InterractableObject fire = null;
		do {
			fire = Fires [Random.Range (0, Fires.Count)];
		} while(fire == current);
		fire.Activated = true;
		fire.gameObject.SetActive (true);
		Indicator.Fire = fire;	
	}

}
