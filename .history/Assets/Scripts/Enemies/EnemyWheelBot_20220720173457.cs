using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWheelBot : MonoBehaviour
{



    public Rigidbody2D rig;
    public Transform collPoint;
    public Animator anim;
    public LayerMask playerLayer;
    public GameObject bullet;
    public Transform bulletPoint;
    public HealthBarEnemy healthBar;
    public GameObject deathEffect;
    public GameObject coinLoot;
    

    private Vector2 direction;
    private GameController gameController;
    


    public float health;
    public float maxHealth;
    public float speed;
    public float rayDetect;
    public float jumpForce;
    public float fireRate = 0.1f;

    private float nextFireTime;
    private int hasDead;
    private float currentSpeed;
    private bool isRight = true;
    

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.totalEnemies += 1;

        currentSpeed = speed;
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
    }



    void Update()
    {
        Flip();
        DetectPlayer();
    }


    void FixedUpdate()
    {
        rig.MovePosition(rig.position + direction * currentSpeed * Time.deltaTime);
    }


    private void Flip()
    {
        if (isRight)
        {
            direction = Vector2.right;
            transform.eulerAngles = new Vector2(0, 0);
            
        }
        else
        {
            direction = Vector2.left;
            transform.eulerAngles = new Vector2(0, 180);

            
        }
    }


    void DetectPlayer() 
    {

        RaycastHit2D hitPlayerLeft = Physics2D.Raycast(collPoint.position, Vector2.left, rayDetect, playerLayer);
        RaycastHit2D hitPlayerRight = Physics2D.Raycast(collPoint.position, Vector2.right, rayDetect, playerLayer);

        if(hitPlayerRight.collider != null)
        {

            currentSpeed = 0;
            isRight = true;

            anim.SetTrigger("shoot");
                
            OnAttack();

        }
        else if(hitPlayerLeft.collider != null) {

            currentSpeed = 0;
            isRight = false;

            anim.SetTrigger("shoot");
                
            OnAttack();
        }
        else 
        {

            currentSpeed = speed;

            anim.SetTrigger("run");
        }
        
        
    }


    void OnAttack()
    {
        if(nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletPoint.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
            //StartCoroutine(AnimationShoot());
        } 
        
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health, maxHealth);

        if (health <= 0) 
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);

            Instantiate(coinLoot, transform.position, Quaternion.identity);

            Destroy(gameObject);
        
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checa se está colidindo com algum obstáculo
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 9)
        {
            isRight = !isRight;
        }
        
    }
}
