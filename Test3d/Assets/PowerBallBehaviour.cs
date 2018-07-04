using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBallBehaviour : MonoBehaviour {

    public PlatformMovement3DA51 activePlayer;
    public Collider triggerArea;
    public readonly string containerName = "PowerContainer";


    public readonly Vector3 idlePoint = new Vector3(0.5f, 0.75f, 0);


    bool waitforNextAction = false;
        

	// Use this for initialization
	void Start () {
        if(activePlayer) {
            triggerArea.enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		


	}

    public void AttackRoundAbout(){
        GetComponent<Animator>().
        transform.parent.localPosition = Vector3.zero;
        GetComponent<Animator>().SetTrigger("Roundabout");

    }

	public void ResetPoint()
	{
        transform.parent.position = idlePoint;
	}


	public void AssignActivePlayer(PlatformMovement3DA51 targetPlayer){
        activePlayer = targetPlayer;
        transform.SetParent(activePlayer.transform.Find(containerName));
        transform.localPosition = idlePoint;
        transform.rotation = targetPlayer.transform.rotation;
        triggerArea.enabled = false;
        GetComponent<Animator>().SetFloat("idleSpeed",0.5f);
    }
}
