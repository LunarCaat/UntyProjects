using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour {
    
    public float paralaxSpeed;
    public bool isParalax;

    private Transform cameraTransform;
 

    private Vector3 lastCameraPos;

    // Use this for initialization
    void Start () {
        cameraTransform = Camera.main.transform;
        lastCameraPos=cameraTransform.position;

	}
	
	// Update is called once per frame
	void Update () {
        if (isParalax)
        {
            Vector3 deltaPos = cameraTransform.position - lastCameraPos;
            transform.position += deltaPos * paralaxSpeed;
        }
        lastCameraPos = cameraTransform.position;

    }

}
