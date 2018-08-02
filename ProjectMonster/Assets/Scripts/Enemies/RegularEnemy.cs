using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class RegularEnemy : EnemyEntity {

    public enum RegularState {
        Patrol,
        Follow,
        Death
    }

    Transform followTarget;
    float resetFollowTime = 3f;
    float currentFollowTime = 0f;
    bool insideFollowReach = false;
    Vector3 planarTargetDistance { get { return new Vector3 (followTarget.transform.position.x, transform.position.y, followTarget.transform.position.z); } }
	public Animator animator2d;
	
    protected override void Start () {
        base.Start ();
        enemyStateMachine = FSM.Create (3, 3);
        enemyStateMachine.SetRelation (0, 0, 1);
        enemyStateMachine.SetRelation (1, 1, 0);
        enemyStateMachine.SetRelation(0, 2, 2);
        enemyStateMachine.SetRelation(1, 2, 2);
        SetCurrentBehaviour (GetBehaviourByIndexName (enemyStateMachine.currentStateIndex));
    }

    protected override void Update () {
        base.Update ();
        SetRenderColor (colorIndex);
    }

    protected override void SendEnemyEvent (int eventIndex) {
        base.SendEnemyEvent (eventIndex);
        SetCurrentBehaviour (GetBehaviourByIndexName (enemyStateMachine.currentStateIndex));
    }

    public EnemyBehaviour GetBehaviourByIndexName (int targetIndex) {
        MethodInfo methodInfo = GetType().GetMethod(((RegularState) targetIndex).ToString(), BindingFlags.Instance | BindingFlags.NonPublic);
        Debug.Log ("__" + methodInfo.Name);
        return (EnemyBehaviour) System.Delegate.CreateDelegate (typeof (EnemyBehaviour), this, methodInfo);
    }

    void Patrol () {
		if(animator2d)
		animator2d.SetInteger("moveState", 0);
        Debug.Log ("Im on Patrol");
        //transform.Rotate (Vector3.up * 85f * Time.deltaTime);
    }

    void Follow () {
		if(animator2d)
		animator2d.SetInteger("moveState", 1);
        Debug.Log ("Following a target");
        Vector3 currentTargetDistance = planarTargetDistance - transform.position;
        if (currentTargetDistance.magnitude >= 3f) {
            Vector3 characterForward = (currentTargetDistance).normalized;
            transform.position += characterForward * speed * Time.deltaTime;
        }
        if (!insideFollowReach) {
            currentFollowTime += Time.deltaTime;
        }
        if (currentFollowTime >= resetFollowTime) {
            currentFollowTime = 0f;
            insideFollowReach = false;
            followTarget = null;
            SendEnemyEvent (1);
        }
    }
    void Death(){
		if(animator2d)
		animator2d.SetInteger("moveState", 2);
        //PlayDeathAnimation
        //When finish stop
    }
	
	public override void TakeDamage (int damage =1, string effectName = null) {
        if (!invulnerable) {
            Debug.Log ("TakeDamage!");
            hp -= damage;
            //GetComponent<Animator> ().SetTrigger ("TakeDamage");
			if (hp <= 0) {
                invulnerable = true;
				QuestManager.instance.Check ("destroy", gameObject.tag);
				SendEnemyEvent (2);
			}
			
            //invulnerable = true;

            if (effectName != null) {
                Debug.Log ("Will try to apply " + effectName);
                effect = EffectManager.instance.Search (effectName).Apply (this);
            }
        }
    }
	

    public override void TriggerEnterCall (GameObject objRef) {
        followTarget = objRef.transform;
        insideFollowReach = true;
        currentFollowTime = 0f;
        SendEnemyEvent (0);
		//Debug.Log("Esta en rango!");
    }

    public override void TriggerExitCall (GameObject objRef) {
        insideFollowReach = false;
    }
	
	public void DestroyThis(){
		Destroy (gameObject);
	}
	
}
