using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour {
    public Transform sightDirection;
    private Transform player;
    private float timer = 0;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        sightDirection.up = (player.position - transform.position).normalized;

        if (timer < 10)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= 10;
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
        

        tempRenderer.sprite = SpriteManagerScript.current.playerBulllet;
        //Destroy(tempRenderer,2);
        TopDownCamMovement camera = Camera.main.GetComponent<TopDownCamMovement>();
        camera.speed = 25;
        camera.impulseDirection = sightDirection.up;

        tempObj.SetActive(true);



    }
}
