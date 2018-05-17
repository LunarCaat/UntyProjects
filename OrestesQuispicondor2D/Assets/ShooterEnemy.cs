using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour {
    public Transform sightDirection;
    private Transform player;
    private float timer = 2.5f;
    public float shootDelay = 5f;
    public SpriteRenderer spriteRendered;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").transform;
        enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        sightDirection.up = (player.position - transform.position).normalized;

        
        if (timer < shootDelay)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= shootDelay;
            Shoot();
        }



    }


    void Shoot()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).ToString());
        //SpriteRenderer tempRenderer= Instantiate(bullet, sightDirection.Find("Cannon").position, sightDirection.rotation).GetComponent<SpriteRenderer>();

        GameObject tempObj = ObjectPoolerScript.current.GetPooledObject();
        tempObj.transform.position = sightDirection.Find("Cannon").position;
        tempObj.transform.rotation = sightDirection.rotation;


        BulletBehaviour bullet = tempObj.GetComponent<BulletBehaviour>();
        bullet.type = BulletBehaviour.BulletType.Enemy;


        SpriteRenderer tempRenderer = tempObj.GetComponent<SpriteRenderer>();

        tempRenderer.color = spriteRendered.color;
        tempRenderer.sprite = SpriteManagerScript.current.playerBulllet;
        //Destroy(tempRenderer,2);
        TopDownCamMovement camera = Camera.main.GetComponent<TopDownCamMovement>();
        camera.speed = 25;
        camera.impulseDirection = sightDirection.up;

        tempObj.SetActive(true);



    }

    void OnBecameVisible()
    {
        enabled = true;
        Debug.Log("Hey I'm visible again!");
    }
    void OnBecameInvisible()
    {
        enabled = false;
        Debug.Log("Hey I'm invisible!");
    }

}
