using UnityEngine;
using System.Collections;

public class ControllerScript : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public float ThrowSpeed = 2.0f;
    public float LerpSeed = 2f;

	private bool objectGrabbed = false;

    private GameObject toolPosition;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        toolPosition = transform.Find("ToolPosition").gameObject;
		device = SteamVR_Controller.Input((int) trackedObject.index);
    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
		if (col.tag == "Grabbable")
        {
			if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && !objectGrabbed)
            {
				objectGrabbed = true;

                col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                col.gameObject.transform.SetParent(gameObject.transform);
                StartCoroutine(LerpToHand(col.gameObject));
                
                var obj = col.gameObject.GetComponent<GrabbableObject>();
                if (obj != null)
                {
                    obj.Device = device;
                    obj.Grabbed = true;
					transform.Find ("Model").gameObject.SetActive(false);
                }
            }
			if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && objectGrabbed)
            {
				objectGrabbed = false;

                col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                col.gameObject.transform.SetParent(null);
                TossObject(col.attachedRigidbody);

                var obj = col.gameObject.GetComponent<GrabbableObject>();
				if (obj != null) {
					obj.Grabbed = false;
					transform.Find ("Model").gameObject.SetActive(true);
				}
            }
        }
    }

    public void TossObject(Rigidbody rigidBody)
    {
        rigidBody.velocity = device.velocity* ThrowSpeed;
        rigidBody.angularVelocity = device.angularVelocity;
    }

    IEnumerator LerpToHand(GameObject GrabbableObject)
    {
		
		while (Vector3.Distance(GrabbableObject.transform.position, toolPosition.transform.position) >= 0.01f 
			|| Quaternion.Angle(GrabbableObject.transform.rotation, toolPosition.transform.rotation) >= 0.01f)
        {
			GrabbableObject.transform.position = Vector3.Lerp(GrabbableObject.transform.position, toolPosition.transform.position, LerpSeed);
			GrabbableObject.transform.rotation = Quaternion.Lerp(GrabbableObject.transform.rotation, toolPosition.transform.rotation, LerpSeed);
			if (objectGrabbed)
            	yield return null;
        }
    }
}