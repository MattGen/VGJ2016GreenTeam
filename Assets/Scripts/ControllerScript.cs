using UnityEngine;
using System.Collections;

public class ControllerScript : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public float ThrowSpeed = 2.0f;
    public float LerpSeed = 2f;

    private GameObject toolPosition;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        toolPosition = transform.Find("ToolPosition").gameObject;
    }

    void Update()
    {
        device = SteamVR_Controller.Input((int) trackedObject.index);
    }

    void OnTriggerStay(Collider col)
    {
		if (col.tag == "Grabbable")
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                col.gameObject.transform.SetParent(gameObject.transform);
                StartCoroutine(LerpToHand(col.gameObject));
            }
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                col.gameObject.transform.SetParent(null);
                TossObject(col.attachedRigidbody);
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
        var startingPos = GrabbableObject.transform.localPosition;
        while (Vector3.Distance(GrabbableObject.transform.localPosition, toolPosition.transform.localPosition) >= 0.1f )
        {
            GrabbableObject.transform.localPosition = Vector3.Lerp(startingPos, toolPosition.transform.localPosition, LerpSeed);
            yield return null;
        }
    }
}