using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour {


    public float horizontalSpeed;
    Vector3 movement;
    public Rigidbody rigidbody3D;
    public float impulseValue;
    public bool isGrounded;
    public float rayDetectionDistance = 0.3f;
    Vector3 leftNode { get { return transform.position - new Vector3(0.3f, 1, 0); } }
    Vector3 rightNode { get { return transform.position + new Vector3(0.3f, -1, 0); } }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool downLeft = Physics.Raycast(leftNode, Vector3.down, rayDetectionDistance);
        bool downRight = Physics.Raycast(rightNode, Vector3.down, rayDetectionDistance);
        movement = transform.position;
        float horizontalDirection = Input.GetAxis("Horizontal");
        float verticalDirection = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.J)){
            transform.Rotate(Vector3.up * 100f * Time.deltaTime);
        }


        if(horizontalDirection!=0||verticalDirection!=0){
            movement += (Vector3.forward*verticalDirection+Vector3.right*horizontalDirection)*horizontalSpeed*Time.deltaTime;
        }

        if (isGrounded)
        {
            Debug.Log("It's Grounded");
            if (!downLeft && !downRight)
            {
                
                isGrounded = false;
            }
            else if (Input.GetKeyDown(KeyCode.H))
                
            {
                Debug.Log("Jump!");
                //verticalSpeed = impulseValue;
                rigidbody3D.AddForce(Vector3.up * impulseValue, ForceMode.Impulse);
                isGrounded = false;
            }
        }
        else
        {
            Debug.Log("Else");
            if (downLeft || downRight)
            {
                Debug.Log("Raycast is Hit");
                isGrounded = true;
            }
        }


        rigidbody3D.MovePosition(movement);
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(leftNode, 0.2f);
        Gizmos.DrawSphere(rightNode, 0.2f);
        Gizmos.color = Color.white;
        Gizmos.DrawRay(leftNode, Vector3.down * rayDetectionDistance);
    }
}
