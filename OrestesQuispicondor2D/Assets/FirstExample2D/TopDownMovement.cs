using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour {
    public float speed =10;
    public float angleVelocity = 100;

    public GameObject bullet;

    public List<Color> colors = new List<Color>();
    int colorIndex =0;
    public int ColorIndex { get { return colorIndex; } }

    Vector3 mouseWorldPos;
    public SpriteRenderer spriteRendered;
    public Transform sightDirection;
    public Transform sightObject;
    public LineRenderer sightLine;


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

        transform.Translate((Vector3.right * GetAxis("Horizontal") + Vector3.up * GetAxis("Vertical")) * speed * Time.deltaTime, Space.World);
        //sightDirection.Rotate(Vector3.back * GetAxis("Arrow_H") * angleVelocity*Time.deltaTime);


        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        


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
            Debug.Log(colorIndex);

        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }
    void LateUpdate()
    {
        sightObject.position = (Vector3.Distance(mouseWorldPos, transform.position) >= 1) ? mouseWorldPos : transform.position + sightDirection.up;
    }

    void Shoot()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).ToString());
        SpriteRenderer tempRenderer= Instantiate(bullet, sightDirection.Find("Cannon").position, sightDirection.rotation).GetComponent<SpriteRenderer>();
        tempRenderer.color= spriteRendered.color;
        Destroy(tempRenderer,2);
        TopDownCamMovement camera= Camera.main.GetComponent<TopDownCamMovement>();
        camera.speed = 25;
        camera.impulseDirection = sightDirection.up;
    }

    void MoveColor(float moveValue)
    {
        moveValue *= 10;
        //int tempValue = colorIndex+(int)moveValue;
        for (int i=0;i<Mathf.Abs(moveValue);i++)
        {
            colorIndex += 1 * (int)Mathf.Sign(moveValue);
            if (colorIndex >= colors.Count)
            {
                colorIndex = 0;
            } else if (colorIndex < 0)
            {
                colorIndex = colors.Count - 1;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            Debug.Log("BlockCollision");
        }
    }
}
