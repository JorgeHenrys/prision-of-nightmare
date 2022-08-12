using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{


    [Header("Components")]
    private GameController gameController;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rig;
    public Animator anim;
    public GameObject bloodEffect;
    public Transform groundPoint;

    public LayerMask groundLayer;
    

    [Header("Stats")]
    public float speed;
    
    public float health;
    public int stamine;
    public float jumpForce;

    public float rayDetectGround;

    bool isJumping;

    public bool isVulnerable;

    public bool roll;
    public float areaAttack;

    float direction;

    public bool isCrouched = false;
    public bool isPushed = false;

    public bool isGrounded;

    public bool hasDashing = false;

    public bool isStopped = false;

    float doubleTapTime;
    KeyCode lastKeyCode;

    public float dashSpeed;
    private float dashCount;
    public float startDashCount;
    private int side;

    private int nJump = 2;
    public float jumpSpeed = 4f;

    public ParticleSystem dust;



    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        if(SceneManager.GetActiveScene().name != "RoomBob"){
            dashCount = startDashCount;
        }
        
    }


    void Update()
    {
        if (isStopped == false)
        {
            direction = Input.GetAxis("Horizontal");
            rig.velocity = new Vector2(direction * speed, rig.velocity.y);

            
            if(direction != 0 && isJumping == false && !isCrouched && isVulnerable)
            {
                if(roll)
                {
                    anim.SetInteger("transition", 5);
                }
                else 
                {
                    anim.SetInteger("transition", 1);
                }
                
            }
            

            if(direction == 0 && isJumping == false && !isCrouched && isVulnerable)
            {
                anim.SetInteger("transition", 0);
            }

            // if (isVulnerable == false)
            // {
            //     anim.SetInteger("transition", 4);
            // }


            if (SceneManager.GetActiveScene().name != "RoomBob")
            {
                OnDashed();
                OnCrouched();
                Jump();
            }

            
        }
        
    }

    


    public void GetHit(float time, float damage)
    {

        health -= damage;
        
        if(health <= 0)
        {
            StartCoroutine(Death(time));

            FindObjectOfType<GameController>().GameOver();
        }
        else
        {
            anim.SetInteger("transition", 4);
            StartCoroutine(Vulnerable(2f));
            FindObjectOfType<GameController>().GetHit(damage);
        }
        
    }

    //float timeInterval = 0.1f;
    IEnumerator Death(float t)
    {
        yield return new WaitForSeconds(t);

        spriteRenderer.enabled = false;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);

        
    }

    IEnumerator Vulnerable(float t)
    {

        Instantiate(bloodEffect, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(t);

        isVulnerable = true;
    }


    void Jump()
    {
        if(isJumping == false)
        {
            nJump = 2;

            if(Input.GetKeyDown(KeyCode.Space) && !isCrouched)
            {
                CreatDust();
                rig.velocity = Vector2.up * jumpForce;
                anim.SetInteger("transition", 2);
                isJumping = true;

            }
        }
        else 
        {
            if(Input.GetKeyDown(KeyCode.Space) && !isCrouched && nJump > 0)
            {
                nJump--;
                CreatDust();
                rig.velocity = Vector2.up * jumpForce;
                anim.SetInteger("transition", 2);
                isJumping = true;

            }
        }
        
    }


    public void OnDashed()
    {

        
        if (side == 0)
        {
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                
               

                if (doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
                {

                    if (stamine >= 5)
                    {
                        CreatDust();
                        stamine -= 5;
                        gameController.UseEstamine();
                     
                        side = 1;

                        //StartCoroutine(UpdateStamine());
                    }
                    
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }

                lastKeyCode = KeyCode.A;
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (doubleTapTime > Time.time & lastKeyCode == KeyCode.D)
                {

                    if (stamine >= 5)
                    {

                        CreatDust();
                        stamine -= 5;
                        gameController.UseEstamine();
                     
                        side = 2;

                        //StartCoroutine(UpdateStamine());
                    }
                    
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }

                lastKeyCode = KeyCode.D;
            }
            
        }
        else
        {

            if (dashCount <= 0)
            {
                side = 0;
                dashCount = startDashCount;
                rig.velocity = Vector2.zero;
                hasDashing = false;
            }
            else 
            {
                dashCount -= Time.deltaTime;
                hasDashing = true;

                if (side == 1)
                {
                    rig.velocity = Vector2.left * dashSpeed;
                }
                else if (side == 2)
                {
                    rig.velocity = Vector2.right * dashSpeed;
                }
            }
            
        }
    }

    public void OnCrouched()
    {

        if(direction == 0 && isJumping == false && isCrouched)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }  
        
    public void OnPushed()
    {   
        
        if(direction != 0 && isJumping == false)
        {
            anim.SetInteger("transition", 7);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RaycastHit2D hasGrounded = Physics2D.Raycast(groundPoint.position, Vector2.down, rayDetectGround, groundLayer);

        if(collision.gameObject.layer == 9 && hasGrounded.collider != null)
        {
            
            isJumping = false;
            isGrounded = true;
            CreatDust();

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            isGrounded = false;
        }
    }
    


    void CreatDust() {
        dust.Play();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 formDown = groundPoint.TransformDirection(Vector2.down) * rayDetectGround;
        Gizmos.DrawRay(groundPoint.position, formDown);

    }

    
}
