using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour {
    //public float angle = -35;
    public Vector3 direction;
    public float speed=30;


    //public float limit;

    public Vector3 limit;
    //private Vector3 initialRotation;
    private Quaternion tempQuaternion;

    private Vector3 rotationRight;
    private Vector3 rotationLeft;
    private Vector3 rotationForward;
    private Vector3 rotationBack;
    //[SerializeField]
    private bool rightAutoMoving=false;
    //[SerializeField]
    private bool leftAutoMoving = false;
    //[SerializeField]
    private bool upAutoMoving = false;
    //[SerializeField]
    private bool downAutoMoving = false;
    private float tempLimit;

    //[SerializeField]
    private char lockedLetter = 'L';
    // Use this for initialization
    void Start () {
        Vector3 initialRotation = transform.rotation.eulerAngles;
        rotationRight = initialRotation + new Vector3(0, -90, 0);
        rotationLeft = initialRotation + new Vector3(0, 90, 0);
        rotationForward = initialRotation + new Vector3(0, 0, 0);
        rotationBack = initialRotation + new Vector3(0, 180, 0);
        //Se asigna la pocision especifica:
        //transform.position = new Vector3(1,0,0);



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


        if (Input.GetKey(KeyCode.A) && transform.position.x > -limit.x && (lockedLetter == 'A' || lockedLetter == 'L'))
        {
            lockedLetter = 'A';
            leftAutoMoving = false;
            transform.eulerAngles = rotationLeft;
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        else if (transform.position.x <= -limit.x)
        {
            transform.position = new Vector3(-limit.x, transform.position.y, transform.position.z);
            leftAutoMoving = false;
            lockedLetter = 'L';
        }
        else if (Input.GetKeyUp(KeyCode.A) && (lockedLetter == 'A' || lockedLetter == 'L'))
        {
            leftAutoMoving = true;
            tempLimit = Mathf.Floor(transform.position.x);
        }


        if (Input.GetKey(KeyCode.D) && transform.position.x < limit.x && (lockedLetter == 'D'|| lockedLetter == 'L'))
        {
            lockedLetter = 'D';
            rightAutoMoving = false;
            transform.eulerAngles = rotationRight;
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        else if (transform.position.x >= limit.x)
        {
            transform.position = new Vector3(limit.x, transform.position.y, transform.position.z);
            rightAutoMoving = false;
            lockedLetter = 'L';
        }
        else if (Input.GetKeyUp(KeyCode.D) && (lockedLetter == 'D' || lockedLetter == 'L'))
        {
            rightAutoMoving = true;
            tempLimit = Mathf.Ceil(transform.position.x);
        }


        if (Input.GetKey(KeyCode.W) && transform.position.z < limit.z && (lockedLetter == 'W' || lockedLetter == 'L'))
        {
            lockedLetter = 'W';
            upAutoMoving = false;
            transform.eulerAngles = rotationBack;
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }
        else if (transform.position.z >= limit.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, limit.z);
            upAutoMoving = false;
            lockedLetter = 'L';
        }
        else if (Input.GetKeyUp(KeyCode.W) && (lockedLetter == 'W' || lockedLetter == 'L'))
        {
            upAutoMoving = true;
            tempLimit = Mathf.Ceil(transform.position.z);
        }


        if (Input.GetKey(KeyCode.S) && transform.position.z > -limit.z && (lockedLetter == 'S' || lockedLetter == 'L'))
        {
            lockedLetter = 'S';
            downAutoMoving = false;
            transform.eulerAngles = rotationForward;
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }
        else if (transform.position.z <= -limit.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -limit.z);
            downAutoMoving = false;
            lockedLetter = 'L';
        }
        else if (Input.GetKeyUp(KeyCode.S) && (lockedLetter == 'S' || lockedLetter == 'L'))
        {
            downAutoMoving = true;
            tempLimit = Mathf.Floor(transform.position.z);
        }


        //Se mueve automaticamente si se levanta el boton hasta la pocision entera mas cercana
        if (rightAutoMoving && transform.position.x < tempLimit && transform.position.x < limit.x)
        {
            
            transform.eulerAngles = rotationRight;
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        if (rightAutoMoving && transform.position.x >tempLimit)
        {
            
            transform.position = new Vector3(tempLimit, transform.position.y, transform.position.z);
            rightAutoMoving = false;
            lockedLetter = 'L';
            
        }

        if (leftAutoMoving && transform.position.x > tempLimit && transform.position.x > -limit.x)
        {

            transform.eulerAngles = rotationLeft;
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        if (leftAutoMoving && transform.position.x < tempLimit)
        {

            transform.position = new Vector3(tempLimit, transform.position.y, transform.position.z);
            leftAutoMoving = false;
            lockedLetter = 'L';
        }

        if (upAutoMoving && transform.position.z < tempLimit && transform.position.z < limit.z)
        {

            transform.eulerAngles = rotationBack;
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }
        if (upAutoMoving && transform.position.z > tempLimit )
        {

            transform.position = new Vector3(transform.position.x, transform.position.y, tempLimit);
            upAutoMoving = false;
            lockedLetter = 'L';
        }

        if (downAutoMoving && transform.position.z > tempLimit && transform.position.z > -limit.z)
        {

            transform.eulerAngles = rotationForward;
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }
        if (downAutoMoving && transform.position.z < tempLimit)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y, tempLimit);
            downAutoMoving = false;
            lockedLetter = 'L';
        }





    }


}
