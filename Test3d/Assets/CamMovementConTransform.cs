using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovementConTransform : MonoBehaviour {

    public Transform lookTarget;
    public float targetHeight;
    public Vector3 targetPoint;
    Vector3 localPoint;
    public float speed;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        localPoint = lookTarget.position+((lookTarget.right * targetPoint.x )+ (lookTarget.up*targetPoint.y)+(lookTarget.forward*targetPoint.z));
        //localPoint = (lookTarget.rotation * new Vector3(1, 1, 1));
        //localPoint.Scale(targetPoint);
        transform.position = Vector3.MoveTowards(transform.position,localPoint, speed*Time.deltaTime);

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
