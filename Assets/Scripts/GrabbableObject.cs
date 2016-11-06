using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum OtherObject{
	Valve,
	Fire,
	None
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
		if (InteractWith == OtherObject.None)
			return;
		
		interactWiths = null;
		switch (InteractWith) {
			case OtherObject.Valve:
				interactWiths = Scheduler.Get ().Valves;
				toolPositionOthers = new List<GameObject> ();
				foreach(var interactWith in interactWiths)
					toolPositionOthers.Add(interactWith.transform.Find("ToolPosition").gameObject);
			break;
			case OtherObject.Fire:
				interactWiths = Scheduler.Get ().Fires;
			break;
		}
        toolPositionMine = transform.Find("ToolPosition").gameObject;

    }

	void OnCollisionEnter(Collision collision){
		var audio = GetComponent<AudioSource> ();
		if(!audio.isPlaying)
			audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (InteractWith == OtherObject.None)
			return;
		
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
					}
				}
				break;
			case OtherObject.Fire:
				if (Device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {
					var audios = GetComponents<AudioSource> ();
					var audio = audios.Length > 1 ? audios [1] : audios [0];
					if(!audio.isPlaying)
						audio.Play();
					Device.TriggerHapticPulse (500);
					var toolPosition = transform.Find ("ToolPosition").Find ("Extinguisher");
					toolPosition.gameObject.SetActive (true);
					transform.Find ("Emmiter").GetComponent<ParticleSystem> ().Play ();
				} else {
					var audios = GetComponents<AudioSource> ();
					var audio = audios.Length > 1 ? audios [1] : audios [0];
					audio.Stop();
					var toolPosition = transform.Find ("ToolPosition").Find ("Extinguisher");
					toolPosition.gameObject.SetActive (false);
					transform.Find ("Emmiter").GetComponent<ParticleSystem> ().Stop ();
				}
				break;
			}
		} else {
			if (InteractWith == OtherObject.Fire) {
				var audios = GetComponents<AudioSource> ();
				var audio = audios.Length > 1 ? audios [1] : audios [0];
				audio.Stop();
				var toolPosition = transform.Find ("ToolPosition").Find ("Extinguisher");
				toolPosition.gameObject.SetActive (false);
				transform.Find ("Emmiter").GetComponent<ParticleSystem> ().Stop ();
			}
		}
	}
}
