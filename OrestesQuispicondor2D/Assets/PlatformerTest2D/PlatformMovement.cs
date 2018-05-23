using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    public float gravity=8;
    public float horizontalSpeed = 2;
    float verticalSpeed;
    public float jumpforce=10;

    bool isGrounded;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isGrounded){
            verticalSpeed -= gravity * Time.deltaTime;
        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            verticalSpeed = jumpforce;
            isGrounded = false;
        }

        transform.Translate(Input.GetAxis("Horizontal")*horizontalSpeed*Time.deltaTime, verticalSpeed*Time.deltaTime, 0);


        Ray2D ray2d = new Ray2D(transform.position,Vector3.down);
        Physics2D.Raycast();

        Debug.DrawRay(transform.position, Vector3.down, Color.green);

	}


	void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Platform")){
            if(verticalSpeed<0)
            isGrounded = true;
            verticalSpeed = 0;
            RayCastHit2D = Physics2D.Raycast(transform.position,Vector3.down).distance;
            ContactFilter2D filter = new ContactFilter2D();
            filter.useTriggers =true;
            float currentDistance = 
            transform.Translate(0,1-currentDistance,0);
        }
       
	}
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            isGrounded = false;
        }

    }
}
