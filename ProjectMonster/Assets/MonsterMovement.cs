using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    public float angularSpeed;
    public float movementSpeed;
    public Rigidbody rigidbodyComponent;
    Vector3 movement;
    Quaternion rotation;

    public Animator animatorController;

    bool grounded;

    float distanceToBackground = 100f;
    Camera viewCamera;
    RaycastHit m_hit;
    public RaycastHit hit
    {
        get { return m_hit; }
    }
    Vector3 cursorPosition;
    Vector3 monsterUp;
    public float cannonDistance =1f;
    public GameObject bullet;

    List<Collider> groundCollection;

	// Use this for initialization
	void Start () {
        viewCamera = Camera.main;
        groundCollection = new List<Collider> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        movement = transform.position;
        rotation = rigidbodyComponent.rotation;
        
        float horizontalMovement = Input.GetAxis ("Horizontal");
        float verticalMovement = Input.GetAxis ("Vertical");

        Quaternion rotationTemp;


        //if (Input.GetKey(KeyCode.J)) {
        //    rotation *= Quaternion.Euler (Vector3.up * -angularSpeed * Time.fixedDeltaTime);
        //}
        //if (Input.GetKey (KeyCode.K)) {
        //    rotation *= Quaternion.Euler (Vector3.up * angularSpeed * Time.fixedDeltaTime);
        //}
        //if (horizontalMovement != 0) {
        //    movement += transform.right * movementSpeed * horizontalMovement * Time.fixedDeltaTime;
        //}
        if (verticalMovement != 0 || horizontalMovement != 0) {
            Vector3 direction = (horizontalMovement * Vector3.right + verticalMovement * Vector3.forward);
            if (direction.magnitude > 1) direction = direction.normalized;
            movement += direction * movementSpeed * Time.fixedDeltaTime;
            //Debug.Log(direction);
            rigidbodyComponent.MovePosition(movement);
            rotationTemp = Quaternion.LookRotation(direction, Vector3.up);
            animatorController.SetFloat("speed",direction.magnitude);
            if(rotationTemp!=rotation){
                //Debug.Log(rotationTemp.eulerAngles);
                rigidbodyComponent.rotation = rotationTemp;
                //Debug.Log(rigidbodyComponent.rotation.eulerAngles);
            }  
        }
       
        
      
        
	}

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            rigidbodyComponent.AddForce (Vector3.up * 10f, ForceMode.Impulse);
        }
        var rayCastHit = RaycastForLayer(Layer.Walkable);
        if (rayCastHit.HasValue)
        {
            m_hit = rayCastHit.Value;
            Vector3 monsterUpTemp = m_hit.point - transform.position;
            monsterUpTemp.y = 0;
            monsterUp = monsterUpTemp.normalized;

            cursorPosition = m_hit.point + Vector3.up * 0.1f;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void OnCollisionStay (Collision collision) {
        if (!groundCollection.Contains (collision.collider)) {
            foreach (ContactPoint contact in collision.contacts) {
                Debug.DrawRay (contact.point, contact.normal * 5f, Color.red, 1f);
                if (Vector3.Dot (contact.normal, Vector3.up) > 0.75f) {
                    Debug.Log ("SHOULD BE GROUNDED!");
                    grounded = true;
                    animatorController.SetBool ("isGrounded", grounded);
                    groundCollection.Add (collision.collider);
                    break;
                }
            }
        }
    }

    void OnCollisionExit (Collision collision) {
        if (groundCollection.Contains(collision.collider)) {
            groundCollection.Remove (collision.collider);
        }
        if (groundCollection.Count <= 0) { 
            grounded = false;
            animatorController.SetBool ("isGrounded", grounded);
        }
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
    void Shoot()
    {
        Instantiate(bullet, transform.position + monsterUp*cannonDistance, Quaternion.LookRotation(monsterUp,Vector3.up));
    }
}
