using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
    public float speed = 1;
	public int damagePower =1;
    // Use this for initialization
    void Start () {
        StartCoroutine(DestroyInSeconds(3f));
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    IEnumerator DestroyInSeconds(float destroyTime){
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other){
		Damageable damagedObject =other.GetComponent<Damageable>();
		if(damagedObject){
			damagedObject.TakeDamage(damagePower);
		}
	}
}
