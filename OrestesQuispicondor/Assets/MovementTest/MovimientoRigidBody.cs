using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Rigidbody playerRB;
    // Use this for initialization
    void Start()
    {
        Vector3 initialRotation = transform.rotation.eulerAngles;
        rotationRight = initialRotation + new Vector3(0, -90, 0);
        rotationLeft = initialRotation + new Vector3(0, 90, 0);
        rotationForward = initialRotation + new Vector3(0, 0, 0);
        rotationBack = initialRotation + new Vector3(0, 180, 0);
        //Se asigna la pocision especifica:
        //transform.position = new Vector3(1,0,0);
        initialPos = transform.position;
        playerRB = GetComponent<Rigidbody>();

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
        ////Rotar usando deltaTime
        ////transform.Rotate(new Vector3(angle, 0, 0)*Time.deltaTime);

        ////Mover usando deltaTime
        //if (transform.position.x >= limit)
        //{
        //    speed = 0;
        //    //Validacion de posicion
        //    //Es importante para corregir imprecisiones de fisica y animacion

        //    //Vector3 transformTemp = transform.position;
        //    //transformTemp.x = limit;
        //    //transform.position = transformTemp;
        //    //Tambien puede ser(mas corto):
        //    //transform.position = new Vector3(limit,transform.position.y,transform.position.z);





        //transform.Translate(direction.normalized * speed * Time.deltaTime);

        //Vector3 currentMovement = Vector3.zero;
        //if (transform.position.x < limit.x)
        //{
        //    currentMovement.x = direction.x * speed;
        //}
        //if (transform.position.y < limit.y)
        //{
        //    currentMovement.y = direction.y * speed;
        //}
        //if (transform.position.z < limit.z)
        //{
        //    currentMovement.z = direction.z * speed;
        //}
        //transform.Translate(currentMovement * Time.deltaTime);




        //if (transform.position.x < limit.x)
        //{
        //    transform.translate(direction.normalized * speed * time.deltatime);
        //}


        if (Input.GetKey(KeyCode.A) && transform.position.x > -limit.x)
        {

            //transform.eulerAngles = rotationLeft;
            tempQuaternion.eulerAngles = rotationLeft;
            playerRB.MoveRotation(tempQuaternion);
            playerRB.MovePosition(transform.position+ (Vector3.left * speed * Time.deltaTime));
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


        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = initialPos;
        }
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    tempLimit= Mathf.Ceil(transform.position.x);
        //}
    }
}
