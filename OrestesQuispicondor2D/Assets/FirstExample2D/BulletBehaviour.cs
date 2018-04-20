using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
    public float speed=1;
    public SpriteRenderer spriteRendered;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up*speed*Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            //other.gameObject.GetComponent<SpriteRenderer>().color = spriteRendered.color;
            //other.gameObject.GetComponent<Collider2D>().enabled = false;
            ChangeColor(other);
            Destroy(other.gameObject,0.5f);
        }
    }

    void ChangeColor(Collider2D other)
    {
        other.gameObject.GetComponent<SpriteRenderer>().color = spriteRendered.color;
        other.gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
