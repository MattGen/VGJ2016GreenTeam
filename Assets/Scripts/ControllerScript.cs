using UnityEngine;
using System.Collections;

public class ControllerScript : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public float throwSpeed = 2.0f;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        device = SteamVR_Controller.Input((int) trackedObject.index);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Wrench")
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                col.gameObject.transform.SetParent(gameObject.transform);
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
        rigidBody.velocity = device.velocity*throwSpeed;
        rigidBody.angularVelocity = device.angularVelocity;
    }
}