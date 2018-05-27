using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    public float gravity=8;
    public float horizontalSpeed = 2;
    public float verticalSpeed;
    public float jumpForce=11;
    public float jetpackForce = 10;
    public float totalGravity = 0;
    public float bottomMaxSpeed = -10;
    public SpriteRenderer spriteRenderer;


    Vector3 downLeftNode { get { return transform.position - new Vector3(0.5f, 1, 0); } }
    Vector3 downRightNode { get { return transform.position + new Vector3(0.5f, -1, 0); } }
    Vector3 sideTopNode { get { return transform.position - new Vector3(1, 0, 0); } }
    Vector3 sideBottomNode { get { return transform.position + new Vector3(1, 1, 0); } }

    bool isGrounded;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float rayDistance = ((verticalSpeed / bottomMaxSpeed)*10)+0.1f;

        RaycastHit2D donwLeft = Physics2D.Raycast(downLeftNode, Vector3.down, rayDistance);
        RaycastHit2D downRight = Physics2D.Raycast(downRightNode, Vector3.down, rayDistance);
        RaycastHit2D sideLeft = Physics2D.Raycast(downLeftNode +new Vector3(0, 0.1f, 0), Vector3.left, 0.1f);
        RaycastHit2D sideRght = Physics2D.Raycast(downRightNode+ new Vector3(0, 0.1f, 0), Vector3.right, 0.1f);

        if (donwLeft || downRight)
        {
            
            CheckReposition(new RaycastHit2D[] { donwLeft, downRight });
        }
        else {
            isGrounded = false;
        }
        float horizontalDirection = Input.GetAxis("Horizontal");
        if (horizontalDirection<0){
            if (!spriteRenderer.flipX) {spriteRenderer.flipX = true;}
            if (sideLeft){
                horizontalDirection = 0;
            }

        }
        if (sideRght && horizontalDirection > 0)
        {
            if (spriteRenderer.flipX) { spriteRenderer.flipX = false; }
            horizontalDirection = 0;
        }

        if (!isGrounded)
        {
            //verticalSpeed -= gravity * Time.deltaTime;
            SubstractSpeed(gravity * Time.deltaTime);
        }
        else {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = jumpForce;
                isGrounded = false;
            }
        }
        transform.Translate(horizontalDirection * horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0);
    }

    void CheckReposition(RaycastHit2D[] nodeRays)
    {
        if (verticalSpeed <= 0)
        {
            foreach (RaycastHit2D ray in nodeRays)
            {
                if (ray)
                {
                    float distance = ray.collider.transform.localScale.y *ray.collider.bounds.size.y / 2;
                    float difference = (downLeftNode.y - ray.collider.transform.position.y) - distance;
                    if (Mathf.Abs(difference)<0.15f)
                    {
                        transform.Translate(0, -difference, 0);
                        isGrounded = true;
                        verticalSpeed = 0;
                    }
                    Debug.DrawLine(transform.position + Vector3.down, ray.collider.transform.position, Color.green);
                    break;
                }
            }
        }
            
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(downLeftNode, 0.2f);
        Gizmos.DrawSphere(downRightNode, 0.2f);
    }

    void SubstractSpeed(float speedToSub){
        float tempSpeed = verticalSpeed - speedToSub;
        Debug.Log(tempSpeed);
        if(tempSpeed<bottomMaxSpeed){
            //Debug.Log("Warning: MAX MIN SPEED!");
            tempSpeed = bottomMaxSpeed;
        }
        verticalSpeed = tempSpeed;
    }

}
