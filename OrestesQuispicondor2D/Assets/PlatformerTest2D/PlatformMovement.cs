using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    public float gravity=8;
    public float horizontalSpeed = 2;
    float verticalSpeed;
    public float jumpForce=11;
    public float jetpackForce = 10;
    public float totalGravity = 0;

    Vector3 leftNode { get { return transform.position - new Vector3(0.5f, 1, 0); }}
    Vector3 rightNode { get { return transform.position + new Vector3(0.5f, -1, 0); } }
    bool isGrounded;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit2D left = Physics2D.Raycast(leftNode,Vector3.down,0.1f);
        RaycastHit2D right = Physics2D.Raycast(rightNode, Vector3.down, 0.1f);

        if(left||right){
            isGrounded = true;
            verticalSpeed = 0;
            CheckReposition(new RaycastHit2D[]{left,right});
        }else{
            isGrounded = false;
        }

        if (!isGrounded)
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = jumpForce;
                isGrounded = false;
            }
        }

        transform.Translate(Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0);

        //Debug.DrawRay(transform.position, Vector3.down, Color.green);


    //    totalGravity = gravity;
    //    if (Input.GetKey(KeyCode.P)){
            
    //        totalGravity -= jetpackForce;   
    //    }
    //    if (totalGravity < 0)
    //        isGrounded = false;
            
    //    if (!isGrounded)
    //    {
    //        verticalSpeed -= totalGravity * Time.deltaTime;
    //    }
    //    else
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            verticalSpeed = jumpForce;
    //            isGrounded = false;
    //        }
    //    }

    //    transform.Translate(Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0);

    //    Debug.DrawRay(transform.position, Vector3.down, Color.green);
    //}

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Platform"))
    //    {
    //        isGrounded = true;
    //        verticalSpeed = 0;
    //        RaycastHit2D[] hits2D = new RaycastHit2D[3];
    //        hits2D[0] =Physics2D.Raycast(transform.position+(Vector3.left*0.5f), Vector3.down);
    //        hits2D[1] =Physics2D.Raycast(transform.position, Vector3.down);
    //        hits2D[2] =Physics2D.Raycast(transform.position+ (Vector3.right * 0.5f), Vector3.down);
    //        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector3.down);
    //        float currentDistance = 0;
    //        if (hit2D != null)
    //        {
    //            currentDistance = hit2D.distance;
    //        }
    //        Debug.DrawRay(transform.position+ (Vector3.left * 0.5f), Vector3.down, Color.green, 1);
    //        Debug.DrawRay(transform.position, Vector3.down, Color.green,1);
    //        Debug.DrawRay(transform.position+ (Vector3.right * 0.5f), Vector3.down, Color.green, 1);


    //        Debug.Log(currentDistance);
    //        transform.Translate(0, 1 - currentDistance, 0);
    //    }
    //}
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Platform"))
    //    {
    //        RaycastHit2D[] hits2D = new RaycastHit2D[3];
    //        hits2D[0] = Physics2D.Raycast(transform.position + (Vector3.left * 0.5f), Vector3.down);
    //        hits2D[1] = Physics2D.Raycast(transform.position, Vector3.down);
    //        hits2D[2] = Physics2D.Raycast(transform.position + (Vector3.right * 0.5f), Vector3.down);
    //        if (hits2D[0]&&hits2D[1]&&hits2D[2])
    //            isGrounded = false;
    //    }

    }

    void CheckReposition(RaycastHit2D[] nodeRays){
        foreach(RaycastHit2D ray in nodeRays){
            if (ray)
            {
                float distance = ray.collider.transform.localScale.y / 2;
                float difference = (leftNode.y - ray.collider.transform.position.y)- distance;


                if (difference != 0)
                {
                    transform.Translate(0, -ray.distance, 0);
                }
                break;
            }
                
        }

    }

	private void OnDrawGizmos()
	{
        Gizmos.DrawSphere(leftNode,0.2f);
        Gizmos.DrawSphere(rightNode, 0.2f);
	}
}
