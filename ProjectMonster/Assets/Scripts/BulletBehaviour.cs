using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
    public float speed = 1;
	public int damagePower =1;
	
	public enum BulletType { ENEMY, PLAYER};
    public BulletType type=BulletType.PLAYER; 
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
        DestroyThis();
    }
    void OnTriggerEnter(Collider other){
		Damageable damagedObject =other.GetComponent<Damageable>();
		if(damagedObject && ((damagedObject is EnemyEntity&&type==BulletType.PLAYER)||(damagedObject is PlayerEntity&&type==BulletType.ENEMY))){
			damagedObject.TakeDamage(damagePower);
            DestroyThis();
		}
	}

    void DestroyThis(){
        //Cambiar a implementacion con object pool despues
        Destroy(gameObject);
    }
}
