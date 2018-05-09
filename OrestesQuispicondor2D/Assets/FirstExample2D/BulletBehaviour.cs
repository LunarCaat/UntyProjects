using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
    public float speed=1;
    private SpriteRenderer spriteRendered;
    
    // Use this for initialization
    void Start () {
        spriteRendered = GetComponent<SpriteRenderer>();
        Destroy(gameObject,1f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up*speed*Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        SpriteRenderer otherRender = other.gameObject.GetComponent<SpriteRenderer>();
        if (otherRender != null&& other.CompareTag("Block"))
        {
            int targetAmmount= (otherRender.color==spriteRendered.color)?5:2;
            other.GetComponent<BlockEntitity>().DecreaseLife(targetAmmount);
            //Destroy(other.gameObject);
            Destroy(gameObject);
        }

        
    }

    void ChangeColor(Collider2D other, Color colorToChange)
    {
        other.gameObject.GetComponent<SpriteRenderer>().color = spriteRendered.color;
        other.gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
