using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour {
    //public float angle = -35;
    public Vector3 direction;
    public float speed=30;


    //public float limit;

    public Vector3 limit;
    // Use this for initialization
    void Start () {
        
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

        Vector3 currentMovement = Vector3.zero;
        if (transform.position.x < limit.x)
        {
            currentMovement.x = direction.x * speed;
        }
        if (transform.position.y < limit.y)
        {
            currentMovement.y = direction.y * speed;
        }
        if (transform.position.z < limit.z)
        {
            currentMovement.z = direction.z * speed;
        }
        transform.Translate(currentMovement * Time.deltaTime);




        //if (transform.position.x < limit.x)
        //{
        //    transform.translate(direction.normalized * speed * time.deltatime);
        //}

    }
}
