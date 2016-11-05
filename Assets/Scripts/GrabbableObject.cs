using UnityEngine;
using System.Collections;

public class GrabbableObject : MonoBehaviour
{

    public GameObject InteractWith;

    [HideInInspector]
    public bool Grabbed = false;
    [HideInInspector]
    public SteamVR_Controller.Device Device;

    private GameObject toolPositionMine;
    private GameObject toolPositionOther;

    void Start()
    {
        toolPositionMine = transform.Find("ToolPosition").gameObject;
        toolPositionOther = InteractWith.transform.Find("ToolPosition").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Grabbed)
	    {
	        if(Device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)
                    && Vector3.Distance(toolPositionMine.transform.position, toolPositionOther.transform.position) < 0.1f)
	        {
				var obj = toolPositionOther.transform.parent.GetComponent<InterractableObject> ();
				if (obj != null) {
					if (!obj.Rotate ())
						return;
				}
				toolPositionOther.transform.parent.Rotate(new Vector3(0,0,1f));
	        }
	    }
	}
}
