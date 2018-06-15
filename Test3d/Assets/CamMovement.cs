using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour {

    public Transform lookTarget;
    public float targetHeight;
    public Vector3 cameraPoint;
    public float speed=5f;
    Vector3 initialDirection;
	// Use this for initialization
	void Start () {
        initialDirection = lookTarget.forward;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Quaternion cameraRotation = Quaternion.FromToRotation(initialDirection, lookTarget.forward);
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(cameraRotation);
        Vector3 rotatedCameraPoint = rotationMatrix.MultiplyPoint3x4(cameraPoint);

        transform.position = Vector3.MoveTowards(transform.position, lookTarget.position + rotatedCameraPoint, speed * Time.deltaTime);
        transform.LookAt(lookTarget);



        //transform.LookAt(lookTarget);
        //float difference = Mathf.Abs(transform.position.y-lookTarget.position.y);
        //if (difference !=targetHeight){
        //    Vector3 temp = transform.position;
        //    temp.y = Mathf.MoveTowards(temp.y, lookTarget.position.y+ targetHeight, 5f * Time.deltaTime);
        //    transform.position = temp;
        //}

        //if(transform.position.x != lookTarget.position.x){
        //    Vector3 temp = transform.position;
        //    temp.x = Mathf.MoveTowards(temp.x,lookTarget.position.x,5f*Time.deltaTime);
        //    transform.position = temp;
        //}
	}
}
