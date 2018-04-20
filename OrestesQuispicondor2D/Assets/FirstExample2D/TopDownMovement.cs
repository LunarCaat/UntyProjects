using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour {
    public float speed =10;

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
        axisList.Add(new Axis("Horizontal", KeyCode.A, KeyCode.D));
        axisList.Add(new Axis("Vertical", KeyCode.S, KeyCode.W));
    }
	
	// Update is called once per frame
	void Update () {
        //transform.Translate((Vector3.right* Input.GetAxis("Horizontal")+ Vector3.forward * Input.GetAxis("Vertical")) * speed *Time.deltaTime);

        transform.Translate((Vector3.right * GetAxis("Horizontal") + Vector3.up * GetAxis("Vertical")) * speed * Time.deltaTime);

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
