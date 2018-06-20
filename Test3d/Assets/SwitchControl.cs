using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour {

    public Transform platform;
    public Vector3 targetPoint;
    public float transitionSpeed;

    bool _enabled;
    public bool startEnabled;
    public bool isEnabled { get { return _enabled; }}
    public Color enabledColor;
    public Color disabledColor;
    Renderer objectRenderer;

    delegate void SwitchSetDelegate(bool setValue);  

	private void Awake()
	{
        objectRenderer = GetComponent<Renderer>();
        SetEnabled(true);
	}

	// Use this for initialization
	void Start () {
		
	}
    public void SetEnabled (bool enabledState){
        objectRenderer.material.color = enabledState ? enabledColor : disabledColor;
        _enabled = enabledState;
    }
    public void SayDebug(bool canSay){
        Debug.Log(canSay);
    }

	// Update is called once per frame
	void Update () {
		
	}
    public void Activate (){
        if(isEnabled){
            Debug.Log("Activated " + name);
            //CALL IENUMERATOR HERE ON A COROUTINE
            StartCoroutine(MovePlatformToTargetPoint(SetEnabled,true));
            SetEnabled(false);
        }else{
            StartCoroutine(MovePlatformToTargetPoint(SayDebug, false));
        }

    }



    IEnumerator MovePlatformToTargetPoint(SwitchSetDelegate callback, bool setValue =false){
        while(platform.position!=targetPoint){
            platform.position = Vector3.MoveTowards(platform.position, targetPoint, transitionSpeed * Time.deltaTime);
            yield return null;
        }
        if(callback!=null){
            callback.Invoke(setValue);
        }
        yield return null;
    }


	private void OnTriggerEnter(Collider other)
	{
		
	}
}
