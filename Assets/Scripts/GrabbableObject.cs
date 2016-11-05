using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum OtherObject{
	Valve,
	Fire
}

public class GrabbableObject : MonoBehaviour
{
	public OtherObject InteractWith;
	private List<InterractableObject> interactWiths;

    [HideInInspector]
    public bool Grabbed = false;
    [HideInInspector]
    public SteamVR_Controller.Device Device;

    private GameObject toolPositionMine;
	private List<GameObject> toolPositionOthers;

    void Start()
    {
		interactWiths = null;
		switch (InteractWith) {
			case OtherObject.Valve:
				interactWiths = Scheduler.Get ().Valves;
			break;
			case OtherObject.Fire:
				interactWiths = Scheduler.Get ().Fires;
			break;
		}
        toolPositionMine = transform.Find("ToolPosition").gameObject;
		toolPositionOthers = new List<GameObject> ();
		foreach(var interactWith in interactWiths)
			toolPositionOthers.Add(interactWith.transform.Find("ToolPosition").gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		if (Grabbed) {
			switch (InteractWith) {
			case OtherObject.Valve:
				foreach (var toolPositionOther in toolPositionOthers) {
					if (Device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)
					     && Vector3.Distance (toolPositionMine.transform.position, toolPositionOther.transform.position) < 0.1f) {
						var obj = toolPositionOther.transform.parent.GetComponent<InterractableObject> ();
						if (obj != null) {
							if (!obj.Rotate ()) {
								Device.TriggerHapticPulse (500);
								return;
							}
						}
						toolPositionOther.transform.parent.Rotate (new Vector3 (0, 0, 1f));
					}
				}
				break;
			case OtherObject.Fire:
				if (Device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {
					Device.TriggerHapticPulse (500);
					var toolPosition = transform.Find ("ToolPosition");
					toolPosition.gameObject.SetActive (true);
					transform.Find ("Emmiter").GetComponent<ParticleSystem> ().Play ();
				} else {
					var toolPosition = transform.Find ("ToolPosition");
					toolPosition.gameObject.SetActive (false);
					transform.Find ("Emmiter").GetComponent<ParticleSystem> ().Stop ();
				}
				break;
			}
		} else {
			if (InteractWith == OtherObject.Fire) {
				var toolPosition = transform.Find ("ToolPosition");
				toolPosition.gameObject.SetActive (false);
				transform.Find ("Emmiter").GetComponent<ParticleSystem> ().Stop ();
			}
		}
	}
}
