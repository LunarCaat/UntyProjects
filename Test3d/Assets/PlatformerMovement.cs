using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour {


    public float horizontalSpeed;
    Vector3 movement;

    public Rigidbody rigidbody3D;
    public float impulseValue;
    public bool isGrounded;
    Quaternion rotation;
    public float rayDetectionDistance = 0.15f;
    public float angularSpeed = 85f;
    Vector3 leftNode { get { return transform.position - new Vector3(0.3f, 0.9f, 0); } }
    Vector3 rightNode { get { return transform.position + new Vector3(0.3f, -0.9f, 0); } }

    public SwitchControl currentSwitch;
    public Transform movingPlatform;


    public Animator anim2D;
    public SpriteRenderer spriteRenderer;




	// Use this for initialization
	void Start () {
        

	}

    // Update is called once per frame
    void LateUpdate() {
        
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
        if(!isGrounded)
            anim2D.SetInteger("moveState", 2);
        else{
            anim2D.SetInteger("moveState", 1);
            if(horizontalDirection != 0 || verticalDirection != 0){
                if (horizontalDirection < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }else{
                anim2D.SetInteger("moveState", 0);
            }

                
                
        }




        rigidbody3D.MovePosition(movement);
        rigidbody3D.MoveRotation(rotation);
	}
	private void Update()
	{
        bool downLeft = Physics.Raycast(leftNode, Vector3.down, rayDetectionDistance);
        bool downRight = Physics.Raycast(rightNode, Vector3.down, rayDetectionDistance);
        if (isGrounded)
        {
            Debug.Log("It's Grounded");
            if (!downLeft && !downRight)
            {

                isGrounded = false;
                //anim2D.SetInteger("moveState", 2);
            }
            else if (Input.GetKeyDown(KeyCode.Space))

            {
                Debug.Log("Jump!");
                //verticalSpeed = impulseValue;
                rigidbody3D.AddForce(Vector3.up * impulseValue, ForceMode.Impulse);
                isGrounded = false;
                //anim2D.SetInteger("moveState", 2);
            }
        }
        else
        {
            Debug.Log("downLeft = " + downLeft.ToString() + " downRight= " + downRight.ToString());
            if (downLeft || downRight)
            {
                Debug.Log("Raycast is Hit");
                isGrounded = true;
            }
        }
        //El orden de las condiciones importa ya que puede ayudar a evitar comparaciones mas costosas
        //Tambien en el caso de getcomponent , si la condicion antes es != null, no importa
        //que no exista la referencia ya que ni lo va a considerar
        if(currentSwitch!=null &&Input.GetKeyDown(KeyCode.E)){
            currentSwitch.Activate();
        }

	}

	void OnDrawGizmos()
    {
        Gizmos.DrawSphere(leftNode, 0.2f);
        Gizmos.DrawSphere(rightNode, 0.2f);
        Gizmos.color = Color.white;
        Gizmos.DrawRay(leftNode, Vector3.down * rayDetectionDistance);
    }


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
}
