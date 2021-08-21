using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    [Header("Components")]
    private GameController gameController;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rig;
    public Animator anim;
    

    [Header("Stats")]
    public float speed;

    public float health;
    public float jumpForce;

    bool isJumping;

    public bool vulnerable;

    public bool roll;
    public float areaAttack;

    float direction;


    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }


    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        roll = Input.GetKey(KeyCode.RightShift);


        if(direction > 0) 
        {
            transform.eulerAngles = new Vector2(0, 0);

        }
        if(direction < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);


            
            if(roll)
            {
                anim.SetInteger("transition", 5);
            }
            else 
            {
                anim.SetInteger("transition", 1);
            }
            
            
        }
        if(direction != 0 && isJumping == false)
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
        

        if(direction == 0 && isJumping == false)
        {
            anim.SetInteger("transition", 0);
        }

        Jump();
    }

    

    private void FixedUpdate()
    {
        rig.velocity = new Vector2(direction * speed, rig.velocity.y);
    }

    public void GetHit()
    {
        if(vulnerable == false)
        {
            gameController.TakeDamage(health);
            health--;
            vulnerable = true;
            StartCoroutine(Respawn());
            
        }
        
    }

    float timeInterval = 0.2f;
    IEnumerator Respawn()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(timeInterval);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(timeInterval);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(timeInterval);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(timeInterval);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(timeInterval);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(timeInterval);
        vulnerable = false;
    }


    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetInteger("transition", 2);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            isJumping = false;
        }
    }

    
}
