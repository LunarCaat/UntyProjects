using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoRigidBody : MonoBehaviour {
    //public float angle = -35;
    public Vector3 direction;
    public float speed = 30;


    //public float limit;

    public Vector3 limit;
    //private Vector3 initialRotation;
    private Quaternion tempQuaternion;

    private Vector3 rotationRight;
    private Vector3 rotationLeft;
    private Vector3 rotationForward;
    private Vector3 rotationBack;

    private Vector3 initialPos;

    private float tempLimit;
    public Text WinText;
    private bool win = false;
    Rigidbody playerRB;
    // Use this for initialization
    void Start()
    {
        WinText.gameObject.SetActive(false);
        Vector3 initialRotation = transform.rotation.eulerAngles;
        rotationRight = initialRotation + new Vector3(0, -90, 0);
        rotationLeft = initialRotation + new Vector3(0, 90, 0);
        rotationForward = initialRotation + new Vector3(0, 0, 0);
        rotationBack = initialRotation + new Vector3(0, 180, 0);
        //Se asigna la pocision especifica:
        //transform.position = new Vector3(1,0,0);
        initialPos = transform.position;
        playerRB = GetComponent<Rigidbody>();
        tempQuaternion = new Quaternion();
        //Se anade una pocision especifica:
        //transform.position += new Vector3(1,0,0);
        //Tambien se puede hacer asi:
        //transform.Translate();
        //Se sustrae una pocision especifica:
        //transform.position -= new Vector3(1,0,0);

        //Se rota por medio de asignar el quaternion
        //Quaternion quat = new Quaternion();
        //quat.eulerAngles = new Vector3(45,0,0);
        //transform.rotation = quat;

        //Se rota un angulo
        //transform.Rotate(new Vector3(5, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (!win)
        {
            moveByInput();
        }
    }
    public void moveByInput()
    {
        if (Input.GetKey(KeyCode.A) && transform.position.x > -limit.x)
        {
            Debug.Log(win);
            
            //transform.eulerAngles = rotationLeft;
            tempQuaternion.eulerAngles = rotationLeft;
            Debug.Log(tempQuaternion);
            playerRB.MoveRotation(tempQuaternion);
            playerRB.MovePosition(transform.position + (Vector3.left * speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < limit.x)
        {
            //transform.eulerAngles = rotationRight;
            tempQuaternion.eulerAngles = rotationRight;
            playerRB.MoveRotation(tempQuaternion);
            playerRB.MovePosition(transform.position + (Vector3.right * speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.W) && transform.position.z < limit.z)
        {
            //transform.eulerAngles = rotationBack;
            tempQuaternion.eulerAngles = rotationBack;
            playerRB.MoveRotation(tempQuaternion);
            playerRB.MovePosition(transform.position + (Vector3.forward * speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S) && transform.position.z > -limit.z)
        {
            //transform.eulerAngles = rotationForward;
            tempQuaternion.eulerAngles = rotationForward;
            playerRB.MoveRotation(tempQuaternion);
            playerRB.MovePosition(transform.position + (Vector3.back * speed * Time.deltaTime));
        }
    }
    public void ResetPosition()
    {

            transform.position = initialPos;

    }
    public void Congratulations()
    {
        win = true;
        WinText.gameObject.SetActive(true);
    }
}
