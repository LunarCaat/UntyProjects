using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour {
    public float backgroundSize;
    public float paralaxSpeed;
    public bool isParalax;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    private Vector3 lastCameraPos;

    // Use this for initialization
    void Start () {
        cameraTransform = Camera.main.transform;
        lastCameraPos=cameraTransform.position;
        layers = new Transform[transform.childCount];
        for (int i =0; i< transform.childCount;i++)
        {
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length;
	}
	
	// Update is called once per frame
	void Update () {
        if (isParalax)
        {
            Vector3 deltaPos = cameraTransform.position - lastCameraPos;
            transform.position += deltaPos * paralaxSpeed;
        }
        lastCameraPos = cameraTransform.position;

    }

    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex=layers.Length - 1;
    }
    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex--;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
}
