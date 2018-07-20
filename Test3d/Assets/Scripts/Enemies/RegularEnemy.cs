using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class RegularEnemy : EnemyEntity {

    public enum RegularState{
        Patrol,
        Follow
    }

    Transform followTarget;
    float resetFollowTime = 2f;
    float currentFollowTime = 0;
    bool insideFollowReach = false;


	// Use this for initialization
    protected override void Start(){
        base.Start();
        enemyStateMachine = FSM.Create(2, 2);
        enemyStateMachine.SetRelation(0, 0, 1);
        enemyStateMachine.SetRelation(1, 1, 0);

        SetCurrentBehaviour(GetBehaviourByName(enemyStateMachine.currentStateIndex));
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        SetRenderColor(colorIndex);
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SetCurrentBehaviour(Patrol);
        } else if(Input.GetKeyDown(KeyCode.Alpha1)){
            SetCurrentBehaviour(Follow);
        }
	}
	protected override void SendEnemyEvent(int eventIndex)
	{
        base.SendEnemyEvent(eventIndex);
        SetCurrentBehaviour(GetBehaviourByName(enemyStateMachine.currentStateIndex));
	}

    public EnemyBehaviour GetBehaviourByName(int targetIndex){
        MethodInfo methodInfo = GetType().GetMethod(((RegularState) targetIndex).ToString(), BindingFlags.Instance| BindingFlags.NonPublic);
        Debug.Log("__" + methodInfo);
        return (EnemyBehaviour) System.Delegate.CreateDelegate(typeof(EnemyBehaviour),this, methodInfo);
    }

	void Follow(){
        Debug.Log("Following a target");
        transform.forward = (planarTargetDistance - transform.position).normalized;
        transform.position += transform.forward * 4.5f * Time.deltaTime;
        if(!insideFollowReach){
            currentFollowTime += Time.deltaTime;
        }
        if(currentFollowTime >= resetFollowTime){
            currentFollowTime = 0;
            insideFollowReach = false;
            followTarget = null;
            SendEnemyEvent(1);
        }
    }

    void Patrol(){
        Debug.Log("I'm on Patrol");
        transform.Rotate(Vector3.up * 85f * Time.deltaTime);
    }
    public override void TriggerEnterCall(GameObject objRef)
    {
        followTarget = objRef.transform;
        insideFollowReach = true;
        currentFollowTime = 0;
        SendEnemyEvent(0);
    }
   

}
