using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMovement25D : MonoBehaviour {

    public float angularSpeed;
    public float movementSpeed;
    public Rigidbody rigidbodyComponent;
    Vector3 movement;
    Vector3 lastPosition;
    Quaternion rotation;

    //public Animator animatorController;
    public Animator anim2D;
    public SpriteRenderer spriteRenderer;
    bool grounded;
    EncounterArea currentEncounter;

    Vector3 tempPosition;
    List<Collider> groundCollection;


    float distanceToBackground = 100f;
    Camera viewCamera;
    RaycastHit m_hit;
    public RaycastHit hit
    {
        get { return m_hit; }
    }
    Vector3 cursorPosition;
    Vector3 monsterUp;
    public float cannonDistance = 1f;
    public GameObject bullet;
    public Transform sightObject;



    public List<Color> colors = new List<Color>();
    int colorIndex = 0;
    public int ColorIndex { get { return colorIndex; } }
	
	public float shootDelay=2f;
	private bool isShooting =false;

    // Use this for initialization
    void Start() {
        viewCamera = Camera.main;
        groundCollection = new List<Collider>();
        lastPosition = rigidbodyComponent.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
		if(UIManager.state==UIManager.GameState.NOT_FINISHED){
			
        movement = transform.position;
        rotation = rigidbodyComponent.rotation;

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

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
            //animatorController.SetFloat("speed",direction.magnitude);
            if (rotationTemp != rotation) {
                //Debug.Log(rotationTemp.eulerAngles);
                //rigidbodyComponent.rotation = rotationTemp;
                //Debug.Log(rigidbodyComponent.rotation.eulerAngles);
            }
            tempPosition = rigidbodyComponent.position;
            if (currentEncounter)
            {
                float distance = (tempPosition - lastPosition).magnitude;
                //Debug.Log("Chance:"+ currentEncounter.encounterChance * distance * movementSpeed * 25);
                //Debug.Log("Distance:"+ distance);
                if (Random.value < currentEncounter.encounterChance* distance*movementSpeed*0.25)
                {
                    Debug.Log("Encounter!");
                    SceneManager.LoadScene(currentEncounter.battleScene, LoadSceneMode.Single);
                }
            }

            lastPosition = tempPosition;

        }
        if (!grounded)
            anim2D.SetInteger("moveState", 2);
        else {
            anim2D.SetInteger("moveState", 1);
            if (horizontalMovement != 0 || verticalMovement != 0)
            {
                if (horizontalMovement < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
            else {
                anim2D.SetInteger("moveState", 0);
            }
        }

		}

    }

    void Update() {
		if(UIManager.state==UIManager.GameState.NOT_FINISHED){
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            rigidbodyComponent.AddForce(Vector3.up * 10f, ForceMode.Impulse);
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
        if (Input.GetMouseButton(0)&&!isShooting)
        {
            isShooting=true;
			StartCoroutine(ShootContinuously(shootDelay));
        }
		
        float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelValue != 0)
        {
            MoveColor(-scrollWheelValue);
        }
		}
    }

    void MoveColor(float moveValue)
    {
        moveValue *= 10;
        for (int i = 0; i < Mathf.Abs(moveValue); i++)
        {
            colorIndex += 1 * (int)Mathf.Sign(moveValue);
            if (colorIndex >= colors.Count)
            {
                colorIndex = 0;
            }
            else if (colorIndex < 0)
            {
                colorIndex = colors.Count - 1;
            }
        }
    }

    void LateUpdate()
    {
        Vector3 cursorWithTransformHeight = cursorPosition;
        cursorWithTransformHeight.y = transform.position.y;
        sightObject.position = (Vector3.Distance(cursorWithTransformHeight, transform.position) >= 1) ? cursorPosition : cursorPosition + monsterUp;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Grass"))
        {
            EncounterArea encounter = other.GetComponent<EncounterArea>();
            if (encounter)
            {
                currentEncounter=encounter;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Grass"))
        {
            EncounterArea encounter = other.GetComponent<EncounterArea>();
            if (encounter)
            {
                currentEncounter = null;
            }
        }
    }


    void OnCollisionStay (Collision collision) {
        if (!groundCollection.Contains (collision.collider)) {
            foreach (ContactPoint contact in collision.contacts) {
                Debug.DrawRay (contact.point, contact.normal * 5f, Color.red, 1f);
                if (Vector3.Dot (contact.normal, Vector3.up) > 0.75f) {
                    Debug.Log ("SHOULD BE GROUNDED!");
                    grounded = true;
                    //animatorController.SetBool ("isGrounded", grounded);
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
            //animatorController.SetBool ("isGrounded", grounded);
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
        GameObject bulletObj = Instantiate(bullet, transform.position + monsterUp * cannonDistance, Quaternion.LookRotation(monsterUp, Vector3.up));
        Renderer bulletRenderer = bulletObj.GetComponent<Renderer>();
        BulletBehaviour bulletBehaviour    = bulletObj.GetComponent<BulletBehaviour>();
        bulletRenderer.material.color = colors[colorIndex];
        if (colorIndex==1){
            bulletBehaviour.powerName = "FrostBall";
        }
        if (colorIndex == 0)
        {
            bulletBehaviour.powerName = "FireBall";
        }

    }
	IEnumerator ShootContinuously(float delay){
        while(Input.GetMouseButton(0)){
			yield return new WaitForSeconds(delay);
			Shoot();
		}
        isShooting = false;
	}
	
}
