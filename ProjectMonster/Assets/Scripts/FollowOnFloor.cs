using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOnFloor : MonoBehaviour {
    public Transform target;
    public bool dostartActive =true;
    bool isEnabled=true;
    public Vector3 offset;
    Renderer spriteRenderer;
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<Renderer>();
        SetFollowEnabled(dostartActive);
	}
	
	// Update is called once per frame
	void Update () {
        if(isEnabled)
            copyPosition();
	}

    public void enableThis(){
        isEnabled = true;
        copyPosition();
        spriteRenderer.enabled =true;
    }
    public void disableThis(){
        isEnabled = false;
        spriteRenderer.enabled = false;
    }

    public void SetFollowEnabled(bool dofollow){
        if(dofollow){
            enableThis();
        }else{
            disableThis();
        }
    }

    void copyPosition(){
        transform.position = new Vector3(target.position.x,transform.position.y, target.position.z)+offset;
    }
}
