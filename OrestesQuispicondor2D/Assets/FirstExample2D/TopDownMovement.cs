using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour {
    public float speed =10;
    public float angleVelocity = 100;

    public GameObject bullet;

    public List<Color> colors = new List<Color>();
    int colorIndex =0;

    public SpriteRenderer spriteRendered;

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

        spriteRendered.color = colors[colorIndex];
        axisList.Add(new Axis("Horizontal", KeyCode.A, KeyCode.D));
        axisList.Add(new Axis("Vertical", KeyCode.S, KeyCode.W));
        axisList.Add(new Axis("Arrow_H", KeyCode.LeftArrow, KeyCode.RightArrow));
    }
	
	// Update is called once per frame
	void Update () {
        //transform.Translate((Vector3.right* Input.GetAxis("Horizontal")+ Vector3.forward * Input.GetAxis("Vertical")) * speed *Time.deltaTime);

        transform.Translate((Vector3.right * GetAxis("Horizontal") + Vector3.up * GetAxis("Vertical")) * speed * Time.deltaTime,Space.World);
        transform.Rotate(Vector3.back * GetAxis("Arrow_H") * angleVelocity*Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveColor();
            Debug.Log(colorIndex);

        }

            if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

    }
    void Shoot()
    {
        SpriteRenderer tempRenderer= Instantiate(bullet, transform.Find("Cannon").position, transform.rotation).GetComponent<SpriteRenderer>();
        tempRenderer.color= spriteRendered.color;
        Destroy(tempRenderer,2);
    }

    void MoveColor()
    {
        colorIndex = (colorIndex >= colors.Count-1) ? 0 : colorIndex + 1;
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
