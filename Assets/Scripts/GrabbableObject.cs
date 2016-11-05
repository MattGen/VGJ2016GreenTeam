using UnityEngine;
using System.Collections;

public class GrabbableObject : MonoBehaviour
{

    public GameObject InteractWith;

    [HideInInspector]
    public bool Grabbed = false;

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
	        if (Vector3.Distance(toolPositionMine.transform.position, toolPositionOther.transform.position) < 0.1f)
	        {
	            var curr = toolPositionOther.transform.parent.transform.localRotation;
                curr.eulerAngles = new Vector3(curr.x);
	        }
	    }
	}
}
