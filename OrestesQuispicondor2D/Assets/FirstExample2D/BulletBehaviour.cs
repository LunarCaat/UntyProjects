using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
    public float speed=1;
    private SpriteRenderer spriteRendered;
    private float timer = 0;

    public enum BulletType {Enemy,Player };

    public BulletType type;
    public int damage=1;

    // Use this for initialization
    void Start () {
        spriteRendered = GetComponent<SpriteRenderer>();
        //Destroy(gameObject,1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            DestroyThis();
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")&&type==BulletBehaviour.BulletType.Player) {
            SpriteRenderer otherRender = other.gameObject.GetComponent<SpriteRenderer>();
            if (otherRender != null)
            {

            int targetAmmount = ((otherRender.color == spriteRendered.color) ? 5 : 2)*damage;
            other.GetComponent<BlockEntitity>().DecreaseLife(targetAmmount);
            //Destroy(other.gameObject);
            DestroyThis();
            }
        }
        if (other.CompareTag("Player") && type == BulletBehaviour.BulletType.Enemy)
        {
            other.GetComponent<TopDownMovementWithRigidBody>().damagePlayer(damage);
        }

        
    }

    void ChangeColor(Collider2D other, Color colorToChange)
    {
        other.gameObject.GetComponent<SpriteRenderer>().color = spriteRendered.color;
        other.gameObject.GetComponent<Collider2D>().enabled = false;
    }


    void OnEnable()
    {
        //nothing for now
    }

    void DestroyThis()
    {
        gameObject.SetActive(false);
        timer = 0;
    }
    
    

}
