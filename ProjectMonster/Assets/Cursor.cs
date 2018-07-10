using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {
    private CameraRaycaster camRayCaster;
    // Use this for initialization
    void Start () {
        camRayCaster = GetComponent<CameraRaycaster>();

    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        Debug.Log("The layermask is: "+ camRayCaster.layerHit);
	}
}
