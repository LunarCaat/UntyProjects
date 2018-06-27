using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovementController : MonoBehaviour {


    public float horizontalSpeed;
    Vector3 movement;

    public Rigidbody rigidbody3D;
    public float impulseValue;
    bool isGrounded=false;
    Quaternion rotation;
    public float rayDetectionDistance = 0.15f;
    public float angularSpeed = 85f;

    public SwitchControl currentSwitch;
    public Transform movingPlatform;


    public Animator anim2D;
    public SpriteRenderer spriteRenderer;
    int groundCount = 0;
    Vector3 lastPlatformPos;
    List<Collider> groundCollection = new List<Collider>();

	// Use this for initialization
	void Start () {
        

	}

    // Update is called once per frame
    void FixedUpdate() {
        
        movement = transform.position;
        rotation = rigidbody3D.rotation;
        float horizontalDirection = Input.GetAxis("Horizontal");
        float verticalDirection = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.J)) {
            rotation *= Quaternion.Euler(Vector3.up * -angularSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.K))
        {
            rotation *= Quaternion.Euler(Vector3.up * angularSpeed * Time.deltaTime);
        }

        if (horizontalDirection != 0 || verticalDirection != 0) {
            
            movement += (transform.forward * verticalDirection + transform.right * horizontalDirection).normalized * horizontalSpeed * Time.deltaTime;
        }

        if(movingPlatform!=null){
            Debug.Log((movingPlatform.position - lastPlatformPos).magnitude);
            movement += movingPlatform.position - lastPlatformPos;
        }



        rigidbody3D.MovePosition(movement);
        rigidbody3D.MoveRotation(rotation);
	}
	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Space)&&isGrounded)

        {
            Debug.Log("Jump!");
            rigidbody3D.AddForce(Vector3.up * impulseValue, ForceMode.Impulse);

        }

	}

	/*private void LateUpdate()
	{
		
	}*/

	


	private void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Switch")){
            currentSwitch=other.GetComponent<SwitchControl>();
        } else if(other.CompareTag("MovingPlatform")){
            movingPlatform = other.transform;
        }
	}
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Switch"))
        {
            currentSwitch = null;
        }
    }

	//private void OnCollisionEnter(Collision collision)
	//{
 //       foreach(ContactPoint contact in collision.contacts){
 //           Debug.DrawRay(contact.point, contact.normal*5f, Color.red,1f);
 //           if(Vector3.Dot(contact.normal,Vector3.up)>0.75){
 //               Debug.Log("Should be grounded");
 //               isGrounded = true;
 //               groundCollection.Add(collision.collider);
 //           }
 //       }
	//}
	private void OnCollisionStay(Collision collision)
	{
        if(groundCollection.Contains(collision.collider))
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 5f, Color.red, 1f);
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.75)
            {
                Debug.Log("Should be grounded");
                isGrounded = true;
                groundCollection.Add(collision.collider);
                    break;
            }
        }
	}


	private void OnCollisionExit(Collision collision)
	{
        if(groundCollection.Contains(collision.collider)){
            groundCollection.Remove(collision.collider);
        }

        if(groundCollection.Count <= 0){
            isGrounded = false;
        }
	}
}
