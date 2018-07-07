using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOnFloor : MonoBehaviour {
    public Transform target;
    public bool dostartActive;
    bool isEnabled=true;
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        transform.position = target.position;
    }
}
