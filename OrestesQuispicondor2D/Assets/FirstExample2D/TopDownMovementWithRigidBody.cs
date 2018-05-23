using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovementWithRigidBody : MonoBehaviour {
    public float speed =10;
    public float angleVelocity = 100;
    public static bool winCondition=false;
    public static bool gameOver = false;

    public GameObject bullet;

    public List<Color> colors = new List<Color>();
    int colorIndex =0;
    public int ColorIndex { get { return colorIndex; } }

    Vector3 mouseWorldPos;
    public SpriteRenderer spriteRendered;
    public Transform sightDirection;
    public Transform sightObject;
    public LineRenderer sightLine;
    float timer = 0;
    public Rigidbody2D Rigidbody2D;
    Vector3 velocity;
    public Animator animator;
    public float shootRate=0.1f;
    private bool isShooting=false;

    public int health=12;
    public bool canGetDamaged=true;


    private float canDamageAgaintimer =0f;
    private bool flashToggle=false;

    public float flashSpeed=0.1f;
    public SpriteRenderer characterSprite;

    const string DIR_HORIZONTAL = "Horizontal";
    const string DIR_VERTICAL = "Vertical";


    public bool isMoving {get{return (GetAxis(DIR_HORIZONTAL)!=0 || GetAxis(DIR_VERTICAL)!=0) ;}}

    struct Axis
    {
        public string name;
        public KeyCode negative;
        public KeyCode positive;
        public Axis (string _name,KeyCode _negative,KeyCode _positive)
        {
            name = _name;
            negative = _negative;
            positive = _positive;
        }
    }

    List<Axis> axisList = new List<Axis>();
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        spriteRendered.color = colors[colorIndex];
        axisList.Add(new Axis("Horizontal", KeyCode.A, KeyCode.D));
        axisList.Add(new Axis("Vertical", KeyCode.S, KeyCode.W));
        axisList.Add(new Axis("Arrow_H", KeyCode.LeftArrow, KeyCode.RightArrow));
    }

    // Update is called once per frame
    void Update() {
        //transform.Translate((Vector3.right* Input.GetAxis("Horizontal")+ Vector3.forward * Input.GetAxis("Vertical")) * speed *Time.deltaTime);

        //transform.Translate((Vector3.right * GetAxis("Horizontal") + Vector3.up * GetAxis("Vertical")) * speed * Time.deltaTime, Space.World);
        //sightDirection.Rotate(Vector3.back * GetAxis("Arrow_H") * angleVelocity*Time.deltaTime);

        //velocity = Vector3.zero;
        //velocity.x = GetAxis("Horizontal") * speed;
        //velocity.y = GetAxis("Vertical") * speed;

        Vector3 step = new  Vector3(GetAxis("Horizontal"),GetAxis("Vertical"));
        step*= speed *Time.deltaTime;
        Rigidbody2D.MovePosition(transform.position +step);

        //if(animator.GetFloat(DIR_HORIZONTAL)!=step.x){
        //	animator.SetFloat()
        //}
        animator.SetBool("Moving",isMoving);
        if(isMoving){
        	animator.SetFloat("LastHorizontal",step.x);
        	animator.SetFloat("LastVertical",step.y);

        }
        animator.SetFloat("Horizontal",step.x);
        animator.SetFloat("Vertical",step.y);
        //animator.SetFloat("LastHorizontal",step.x);

        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Anadir version modificada de walking despues
        /*if (GetAxis("Horizontal")==0 && GetAxis("Vertical")==0)
        {
            
                animator.SetBool("Walking", false);
        }
        else 
        {
            animator.SetBool("Walking", true);
        }*/

        mouseWorldPos.z = transform.position.z;
        Debug.DrawLine(transform.position, mouseWorldPos, Color.red);
        //transform.up = (mouseWorldPos - transform.position).normalized;

        //if (Vector3.Distance(mouseWorldPos,transform.position) >= 1)
        //{
        //    sightObject.position = mouseWorldPos;
        //}
        //else
        //{
        //    sightObject.position = transform.position + sightDirection.up;
        //}
        sightDirection.up = (mouseWorldPos - transform.position).normalized;
        sightObject.position = (Vector3.Distance(mouseWorldPos, transform.position) >= 1) ? mouseWorldPos : transform.position + sightDirection.up;

        sightLine.SetPositions(new Vector3[]{ transform.position, (transform.position+sightDirection.up*3)});

        int facing= (Mathf.CeilToInt((sightDirection.rotation.eulerAngles.z+45)/90))%4;
        Debug.Log(facing);
        
        
        //Implementar otro modo de facing despues
        //animator.SetInteger("Facing",facing);


        //sightDirection.Rotate(Vector3.back * GetAxis("Arrow_H") * angleVelocity * Time.deltaTime);

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    MoveColor();
        //    Debug.Log(colorIndex);

        //}
        float scrollWheelValue= Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelValue!=0)
        {
            MoveColor(scrollWheelValue);
            //Debug.Log(colorIndex);

        }

        if (Input.GetMouseButton(0) && !isShooting) {
            isShooting = true;
            StartCoroutine("ContinuousShoot");
        }

        //if (!canGetDamaged)
        //{
        //   canDamageAgaintimer -= Time.deltaTime;
        //   if (canDamageAgaintimer <  0f)
        //   {
        //       canGetDamaged=true;
        //   }

        //}
            //isShooting = Input.GetMouseButton(0);

    }
    void LateUpdate()
    {
        sightObject.position = (Vector3.Distance(mouseWorldPos, transform.position) >= 1) ? mouseWorldPos : transform.position + sightDirection.up;



        //Rigidbody2D.velocity = velocity;

    }

    void Shoot()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).ToString());
        //SpriteRenderer tempRenderer= Instantiate(bullet, sightDirection.Find("Cannon").position, sightDirection.rotation).GetComponent<SpriteRenderer>();

        GameObject tempObj=ObjectPoolerScript.current.GetPooledObject();
        tempObj.transform.position = sightDirection.Find("Cannon").position;
        tempObj.transform.rotation = sightDirection.rotation;


        BulletBehaviour bullet=tempObj.GetComponent<BulletBehaviour>();
        bullet.type = BulletBehaviour.BulletType.Player;

        SpriteRenderer tempRenderer = tempObj.GetComponent<SpriteRenderer>();
        tempRenderer.color= spriteRendered.color;
        
        tempRenderer.sprite = SpriteManagerScript.current.playerBulllet;
        //Destroy(tempRenderer,2);
        TopDownCamMovement camera= Camera.main.GetComponent<TopDownCamMovement>();
        camera.speed = 25;
        camera.impulseDirection = sightDirection.up;

        tempObj.SetActive(true);



    }

    IEnumerator ContinuousShoot()
    {
        while(Input.GetMouseButton(0))
        {
            Shoot();
            yield return new WaitForSeconds(shootRate);
        }
        isShooting = false;
    }
    IEnumerator Flash(float x)
    {
        canGetDamaged = false;
        for (int i = 0; i < 8; i++)
        {
            characterSprite.enabled = false;
            yield return new WaitForSeconds(x);
            characterSprite.enabled = true;
            yield return new WaitForSeconds(x);
        }
        canGetDamaged = true;
    }

    void MoveColor(float moveValue)
    {
        moveValue *= 10;
        int colorsCount= colors.Count;
        //int tempValue = colorIndex+(int)moveValue;
        for (int i=0;i<Mathf.Abs(moveValue);i++)
        {
            colorIndex += 1 * (int)Mathf.Sign(moveValue);
            if (colorIndex >= colorsCount)
            {
                colorIndex = 0;
            } else if (colorIndex < 0)
            {
                colorIndex = colorsCount - 1;
            }
        }

        //colorIndex += (int)moveValue;
        //bool negative = false;
        //if (Mathf.Sign(moveValue) > 0) negative = true;
        //colorIndex = colorIndex % colors.Count;

        /*if(tempValue>= colors.Count)
        {
            colorIndex = 0;
        }else if (tempValue < 0)
        {
            colorIndex = colors.Count - 1;
        }*/


        //colorIndex = (colorIndex >= colors.Count-1) ? 0 : colorIndex + 1;
        spriteRendered.color = colors[colorIndex];

    }
    int GetHorizontalAxis()
    {
        if (Input.GetKey(KeyCode.D))
        {
            return 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            return -1;
        }
        return 0;
    }
    int GetVerticalAxis()
    {
        if (Input.GetKey(KeyCode.W))
        {
            return 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            return -1;
        }
        return 0;
    }

    int GetAxis(string axisName)
    {
        Axis axis = axisList.Find(target => target.name == axisName);


        if (Input.GetKey(axis.positive))
        {
            return 1;
        }
        if (Input.GetKey(axis.negative))
        {
            return -1;
        }
        return 0;
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Block"))
    //    {
    //        Debug.Log("BlockCollision");
    //    }
    //}
    
    public void canDamageAgainLater(){
        canGetDamaged=false;
        canDamageAgaintimer =10f;
    }
    public void damagePlayer(int damage) {
        if (canGetDamaged&&!gameOver) {
            StartCoroutine(Flash(flashSpeed));
            if (health - damage <= 0)
            {
                if (!winCondition)
                {
                    health = 0;
                    gameOver = true;
                    gameObject.SetActive(false);
                    //velocity = Vector3.zero;
                    //enabled = false;
                }
                    

            }
            else
            {
                
                if (!winCondition)
                    health -= damage;
            }
        }
    }


    /*void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj=collision.gameObject;
        if (obj.CompareTag("Enemy")|| obj.CompareTag("EnemyBullet")&&canGetDamaged){
            canGetDamaged=false;
            health--;
            StartCoroutine("canGetDamagedAgain");
        }
    }*/

    void OnTriggerEnter2D(Collider2D other)
    {
        
       
    }
}
