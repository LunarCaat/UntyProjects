using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoClasico : MonoBehaviour {
    public Vector3 direction;
    public float speed = 30;


    public Vector3 limit;
    //Estos valores de rotacion se usaran para que mire hacia una direccion
    //partiendo de como se le dejo antes que se corra el juego
    private Vector3 rotationRight;
    private Vector3 rotationLeft;
    private Vector3 rotationForward;
    private Vector3 rotationBack;
    // Use this for initialization
    void Start () {
        Vector3 initialRotation = transform.rotation.eulerAngles;
        rotationRight = initialRotation + new Vector3(0, -90, 0);
        rotationLeft = initialRotation + new Vector3(0, 90, 0);
        rotationForward = initialRotation + new Vector3(0, 0, 0);
        rotationBack = initialRotation + new Vector3(0, 180, 0);
    }
	
	// Update is called once per frame
	void Update () {
        moveInASquareRegionByInput(speed, limit);
    }
    private void moveTowards(float movementSpeed, Vector3 facingRotation, Vector3 translateDirection)
    {
        transform.eulerAngles = facingRotation;
        transform.Translate(translateDirection * movementSpeed * Time.deltaTime, Space.World);
    }
    //private void mueveIzquierda(float velocidadMovimiento)
    //{
    //    transform.eulerAngles = rotationLeft;
    //    transform.Translate(Vector3.left * velocidadMovimiento * Time.deltaTime, Space.World);
    //}
    //private void mueveDerecha(float velocidadMovimiento)
    //{
    //    transform.eulerAngles = rotationRight;
    //    transform.Translate(Vector3.right * velocidadMovimiento * Time.deltaTime, Space.World);
    //}
    //private void mueveArriba(float velocidadMovimiento)
    //{
    //    transform.eulerAngles = rotationBack;
    //    transform.Translate(Vector3.forward * velocidadMovimiento * Time.deltaTime, Space.World);
    //}
    //private void mueveAbajo(float velocidadMovimiento)
    //{
    //    transform.eulerAngles = rotationForward;
    //    transform.Translate(Vector3.back * velocidadMovimiento * Time.deltaTime, Space.World);
    //}

    private void moveTowardsLeft(float movementSpeed)
    {
        moveTowards(movementSpeed, rotationLeft, Vector3.left);
    }
    private void moveTowardsRight(float movementSpeed)
    {
        moveTowards(movementSpeed, rotationRight, Vector3.right);
    }
    private void moveTowardsUp(float movementSpeed)
    {
        moveTowards(movementSpeed, rotationBack, Vector3.forward);
    }
    private void moveTowardsDown(float movementSpeed)
    {
        moveTowards(movementSpeed, rotationForward, Vector3.back);
    }


    private void moveByInput(float movementSpeed, Vector3 squareLimit)
    {
        if (Input.GetKey(KeyCode.A) && transform.position.x > -squareLimit.x)
        {
            moveTowardsLeft(movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < squareLimit.x)
        {
            moveTowardsRight(movementSpeed);
        }
        if (Input.GetKey(KeyCode.W) && transform.position.z < squareLimit.z)
        {
            moveTowardsUp(movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.z > -squareLimit.z)
        {
            moveTowardsDown(movementSpeed);
        }
    }
    private void validateLimit(Vector3 squareLimit)
    {
        if (transform.position.x <= -squareLimit.x)
        {
            transform.position = new Vector3(-limit.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= squareLimit.x)
        {
            transform.position = new Vector3(limit.x, transform.position.y, transform.position.z);
        }
        if (transform.position.z >= squareLimit.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, limit.z);
        }
        if (transform.position.z <= -squareLimit.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -limit.z);
        }
    }

    private void moveInASquareRegionByInput(float movementSpeed, Vector3 squareLimit)
    {
        moveByInput(movementSpeed, squareLimit);
        validateLimit(squareLimit);
    }
}
