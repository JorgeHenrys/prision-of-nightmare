using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : MonoBehaviour
{
    public Rigidbody2D rig;
    public Animator anim;
    public Transform collPoint;
    private Player player;
    public Collider2D coll;
    public Transform groundPoint;
    

    private Vector2 direction;


    public LayerMask playerLayer;
    public LayerMask groundLayer;



    public float speed;
    public float rayDetect;
    public float attackUp;
    public float rayDetectGround;
    public float jumpForce;

    private float currentSpeed;
    private bool hasActivate;
    private bool hasFirstAwake;
    private bool isRight =  false;

    void Start()
    {
        hasActivate = false;
        hasFirstAwake = false;
        currentSpeed = 0f;
        rig.isKinematic = true;
        coll.isTrigger = true;
        player = FindObjectOfType<Player>();
        
    }

   
    
    void OnAwake()
    {
        if(hasActivate){
            
            if(hasFirstAwake == false)
            {
                hasFirstAwake = true;
                anim.SetTrigger("awake");
                StartCoroutine(StartRun(1f));
            }
            else
            {
                DetectPlayer();
            }
            
        }
    }

    
    void Update()
    {
        
        OnAwake();
        
    }

    void FixedUpdate()
    {

        
        //rig.MovePosition(rig.position + direction * currentSpeed * Time.deltaTime);
    }


    void DetectPlayer() 
    {

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);

        RaycastHit2D hitPlayer = Physics2D.Raycast(collPoint.position, direction, rayDetect, playerLayer);

        if(hitPlayer.collider != null)
        {

                
            OnAttack();

        }
        else 
        {

            currentSpeed = speed;

            anim.SetTrigger("run");
        }

        Flip();
        JumpObstacle();
        
        
    }



   


    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
            direction = Vector2.left;
            
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            
            direction = Vector2.right;
            
            
        }
    }


    private void OnAttack()
    {

        currentSpeed = 0;
        anim.SetTrigger("attack");
        
       // transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        //StartCoroutine(UpAndDownJump(2f)); 
        //rig.MovePosition(player.rig.position + Vector2.up * 4f * 1.5f * Time.deltaTime);

        

    }


    private void JumpObstacle()
    {
        RaycastHit2D hasObstacle = Physics2D.Raycast(groundPoint.position, direction, rayDetectGround, groundLayer);
        if (hasObstacle.collider != null)
        {
            rig.velocity = Vector2.up * jumpForce;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //checa se está colidindo com algum obstáculo
        if (collision.gameObject.layer == 8 && hasActivate == false)
        {
            StartCoroutine(Awakening(2f));
        }

        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 9)
        {
            isRight = !isRight;
        }
    }


    IEnumerator Awakening(float t)
    {
        

        yield return new WaitForSeconds(t);

        hasActivate = true;
    }



    IEnumerator UpAndDownJump(float t)
    {
        
        rig.velocity = Vector2.up * jumpForce * 2f;

        yield return new WaitForSeconds(t);
        
        //anim.SetTrigger("attack-down");

        rig.velocity = Vector2.down * jumpForce * 2f;
    }


    IEnumerator StartRun(float t)
    {
        

        yield return new WaitForSeconds(t);
 
        anim.SetTrigger("run");
        currentSpeed = speed;
        rig.isKinematic = false;
        coll.isTrigger = false;
    }


}
