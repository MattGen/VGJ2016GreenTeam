using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scheduler : MonoBehaviour {

	public Indicators Indicator;
	public List<InterractableObject> Valves;
	public List<InterractableObject> Fires;

	private static Scheduler instance;
	void Awake(){
		instance = this;
	}

	public static Scheduler Get(){
		return instance;
	}

	void Start(){
		ActivateNext (null);
	}

	public void ActivateNext(InterractableObject current){
		InterractableObject valve = null;
		do {
			valve = Valves [Random.Range (0, Valves.Count)];
		} while(valve == current);
		valve.Activated = true;
		Indicator.Valve = valve;
	}
}
