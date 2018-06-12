using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    public float gravity=8;
    public float horizontalMaxSpeed = 2;
    public float verticalSpeed;
    public float horizontalSpeed=0;
    public float jumpForce=11;
    public float jetpackForce = 10;
    public float totalGravity = 0;
    public float bottomMaxSpeed = -10;
    public float rayDetectionDistance = 0.1f;

    public float raydetectionDistance =0.1f;

    private Collider2D thisCollider;
    public Collider2D colliderToIgnore;
    private Animator animator;

    Vector3 leftNode { get { return transform.position - new Vector3(0.3f, 1, 0); } }
    Vector3 rightNode { get { return transform.position + new Vector3(0.3f, -1, 0); } }
    [SerializeField]
    bool isGrounded;
    bool isJumping;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody;
    public bool usesRigidbody;

    //Inicializar estas variables antes
    [SerializeField]
    bool isFalling = true;

    bool isFallingFirstTime=false;
    [SerializeField]
    bool stoppedByWall = false;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        thisCollider=GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        if (!usesRigidbody)
        {
            rigidbody.Sleep();
        }
    }

    void RigidBodyUpdate()
    {
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        RaycastHit2D downLeft = Physics2D.Raycast(leftNode, Vector3.down, rayDetectionDistance);
        RaycastHit2D downRight = Physics2D.Raycast(rightNode, Vector3.down, rayDetectionDistance);

        float horizontalDirection = Input.GetAxis("Horizontal");
        if (horizontalDirection < 0)
        {
            if (!spriteRenderer.flipX) { spriteRenderer.flipX = true; }
        }
        else if (horizontalDirection > 0)
        {
            if (spriteRenderer.flipX) { spriteRenderer.flipX = false; }
        }
        if (isGrounded)
        {
            if (!downLeft && !downRight)
            {
                isGrounded = false;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = jumpForce;
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
        else {
            if (downLeft || downRight)
            {
                isGrounded = true;
            }
        }

        

        rigidbody.velocity = new Vector2(horizontalDirection * horizontalMaxSpeed, rigidbody.velocity.y);
    }

    void TransformUpdate()
    {
        RaycastHit2D downLeft = Physics2D.Raycast(leftNode, Vector3.down, rayDetectionDistance);
        RaycastHit2D downRight = Physics2D.Raycast(rightNode, Vector3.down, rayDetectionDistance);
        RaycastHit2D sideLeft = Physics2D.Raycast(leftNode + new Vector3(0, 0.1f, 0), Vector3.left, 0.1f);
        RaycastHit2D sideRight = Physics2D.Raycast(rightNode + new Vector3(0, 0.1f, 0), Vector3.right, 0.1f);

        /*if (downLeft || downRight) {
            CheckReposition (new RaycastHit2D[] { downLeft, downRight });
        } else { 
            isGrounded = false; 
        }*/

        float horizontalDirection = Input.GetAxis("Horizontal");
        float verticalDirection = Input.GetAxis("Vertical");
        float horizontalVelocity;


        if (!isGrounded)
        {
            animator.SetInteger("moveState", 2);
        }
        else {
            if (horizontalDirection != 0)
            {
                animator.SetInteger("moveState", 1);
            }
            else
            {
                animator.SetInteger("moveState", 0);
            }
        }


        if (horizontalDirection < 0)
        {
            if (!spriteRenderer.flipX) { spriteRenderer.flipX = true; }

            //if (isGrounded)
            if ((horizontalSpeed < 0||stoppedByWall)&&sideLeft && !(sideLeft.collider.OverlapPoint(leftNode + new Vector3(0, 0.1f, 0))))
            //if (sideLeft&&isGrounded)
            {
                stoppedByWall = true;
                horizontalDirection = 0;
            }
            else
            {
                if (stoppedByWall)
                    Debug.Log("Stopped by left wall!");
                stoppedByWall = false;
            }
        }
        else if (horizontalDirection > 0)
        {
            if (spriteRenderer.flipX) { spriteRenderer.flipX = false; }
            //if (isGrounded)
            if ((horizontalSpeed > 0 || stoppedByWall)&&sideRight && !(sideRight.collider.OverlapPoint(rightNode + new Vector3(0, 0.1f, 0))))
            //if (sideRight && isGrounded)
            {
                stoppedByWall = true;
                horizontalDirection = 0;
            }
            else
            {
                Debug.Log("Stopped by right wall!");
                stoppedByWall = false;
            }
        }
        if (!isGrounded)
        {

            verticalSpeed -= gravity * Time.deltaTime;
            if (verticalSpeed < 0)
            {
                if (!isFalling) {
                    isFallingFirstTime = true;
                    isFalling = true;
                }
                rayDetectionDistance = -verticalSpeed * Time.deltaTime;
                CheckVerticalClamp(new RaycastHit2D[] { downLeft, downRight });
                isFallingFirstTime = false;



            }
            else {
                if (rayDetectionDistance != 0.1f)
                {
                    rayDetectionDistance = 0.1f;
                }
                isFallingFirstTime = false;
                isFalling = false;
            }
        }
        else {
            if (!downLeft && !downRight)
            {
                isGrounded = false;
            }
            else {
                Collider2D colliderInRay = null;
                if (downLeft && downRight) {
                    colliderInRay = horizontalDirection > 0 ? downRight.collider : downLeft.collider;
                }
                else
                {
                    colliderInRay = !downLeft ? downRight.collider : downLeft.collider;
                }
                //if (!colliderInRay) Debug.Log("Collider is null!");
                //if (!downRight.collider && !downLeft.collider) Debug.Log("Both colliders are null");

                //Codigo de asignar y limpiar flag de ignorar en el collider anterior
                if (!colliderToIgnore)
                {
                    colliderToIgnore = colliderInRay;
                }
                else if (colliderToIgnore != colliderInRay)
                {
                    Debug.Log("Ignored no more!");
                    Physics2D.IgnoreCollision(colliderToIgnore, thisCollider, false);
                    colliderToIgnore.gameObject.layer = 0;
                    colliderToIgnore = colliderInRay;
                }



                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        if ((Physics2D.RaycastAll(leftNode, Vector3.down, 6).Length > 1 || Physics2D.RaycastAll(rightNode, Vector3.down, 6).Length > 1))
                        {
                            Debug.Log("Jump Down!");
                            //Ignoring collision
                            //Physics2D.IgnoreCollision(colliderToIgnore, thisCollider);
                            //Setting layer
                            horizontalSpeed = 0;
                            colliderToIgnore.gameObject.layer = 2;
                            isGrounded = false;
                            isFalling = true;
                            stoppedByWall = false;
                        }



                    }
                    else {
                        verticalSpeed = jumpForce;
                        isGrounded = false;
                    }

                }
            }
        }

        //if (!isGrounded)
        //{
        //    if (horizontalSpeed < 0)
        //    {
        //        if (sideLeft && !(sideLeft.collider.OverlapPoint(leftNode + new Vector3(0, 0.1f, 0))))
        //        //if (sideLeft&&isGrounded)
        //        {
        //            stoppedByWall = true;
        //            horizontalDirection = 0;
        //        }
        //        else
        //        {
        //            if (stoppedByWall)
        //                Debug.Log("Stopped by left wall!");
        //            stoppedByWall = false;
        //        }
        //    }
        //    else if (horizontalSpeed > 0)
        //    {
        //        if (sideRight && !(sideRight.collider.OverlapPoint(rightNode + new Vector3(0, 0.1f, 0))))
        //        //if (sideRight && isGrounded)
        //        {
        //            stoppedByWall = true;
        //            horizontalDirection = 0;
        //        }
        //        else
        //        {
        //            Debug.Log("Stopped by right wall!");
        //            stoppedByWall = false;
        //        }
        //    }
        //}
        
        


        //Update horizontalSpeed if grounded 
        if (isGrounded || (stoppedByWall ))
        {
            horizontalSpeed = horizontalDirection * horizontalMaxSpeed;
        }

        transform.Translate(horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if(PlatformManager.state == PlatformManager.GameState.NOT_FINISHED)
        {
            if (usesRigidbody)
            {
                RigidBodyUpdate();
            }
            else {
                TransformUpdate();
            }
        }
        
    }

    void CheckVerticalClamp(RaycastHit2D[] nodeRays)
    {
        foreach (RaycastHit2D ray in nodeRays)
        {

            if (ray && rayDetectionDistance > ray.distance)
            {
                if (isFallingFirstTime)
                {
                    
                    if ((ray.collider.OverlapPoint(leftNode)|| ray.collider.OverlapPoint(rightNode))&&ray.collider!=colliderToIgnore)
                    {
                        if (colliderToIgnore)
                        {
                            Physics2D.IgnoreCollision(colliderToIgnore, thisCollider, false);
                            colliderToIgnore.gameObject.layer = 0;
                        }
                        Debug.Log("Previous collider"+colliderToIgnore.name);
                        colliderToIgnore = ray.collider;
                        
                        Physics2D.IgnoreCollision(colliderToIgnore, thisCollider);
                        Debug.Log("Ignored by falling!" + ray.collider.name);
                        //Setting layer
                        colliderToIgnore.gameObject.layer = 2;
                        stoppedByWall = false;
                        break;
                    }
                    
                    
                }
                else
                {
                    if (ray.distance <= float.Epsilon)
                    {
                        float difference = leftNode.y - ray.collider.bounds.ClosestPoint(leftNode).y;
                        transform.Translate(0, -difference, 0);
                        Debug.Log("Success!" + ray.collider.name);
                    }
                    else {
                        transform.Translate(0, -ray.distance, 0);
                    }
                    Collider2D colliderInRay = ray.collider;
                    if (!colliderToIgnore)
                    {
                        colliderToIgnore = colliderInRay;
                    }
                    else if (colliderToIgnore != colliderInRay)
                    {
                        Debug.Log("Ignored no more!");
                        Physics2D.IgnoreCollision(colliderToIgnore, thisCollider, false);
                        colliderToIgnore.gameObject.layer = 0;
                        colliderToIgnore = colliderInRay;
                    }
                    isGrounded = true;
                    isFalling = false;
                    Debug.Log("Stopped!" + ray.collider.name);
                    verticalSpeed = 0;
                    break;
                }
                
            }
        }
    }

    void CheckReposition(RaycastHit2D[] nodeRays)
    {
        Debug.Log(verticalSpeed);
        foreach (RaycastHit2D ray in nodeRays)
        {
            if (ray && verticalSpeed <= 0)
            {
                float distance = ray.collider.transform.localScale.y * ray.collider.bounds.size.y / 2;
                float difference = (leftNode.y - ray.collider.transform.position.y) - distance;
                if (Mathf.Abs(difference) <= 0.5f)
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

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(leftNode, 0.2f);
        Gizmos.DrawSphere(rightNode, 0.2f);
        Gizmos.color = Color.white;
        Gizmos.DrawRay(leftNode, Vector3.down * rayDetectionDistance);
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
