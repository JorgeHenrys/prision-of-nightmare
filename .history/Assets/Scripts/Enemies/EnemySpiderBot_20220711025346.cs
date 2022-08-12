using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpiderBot : MonoBehaviour
{

    public Rigidbody2D rig;
    public Transform collPoint;
    private Player player;
    public LayerMask playerLayer;
    public GameObject bullet;
    public Animator anim;

    private Vector2 direction;
    public HealthBarEnemy healthBar;
    public GameObject deathEffect;
    public GameObject coinLoot;
    



    public float speed;
    public float rangeDetect;
    public float fireRate = 0.1f;
    public float health;
    public float maxHealth;


    private float nextFireTime;
    private float currentSpeed;
    private bool isRight;




    



    void Start()
    {
        currentSpeed = speed;
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
        player = FindObjectOfType<Player>();
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
            transform.eulerAngles = new Vector2(0, 180);
            Debug.Log("DIREITA");
        }
        else
        {
            direction = Vector2.left;
            transform.eulerAngles = new Vector2(0, 0);
            Debug.Log("ESQUERDA");
        }
    }


    void DetectPlayer() 
    {

        RaycastHit2D hitPlayer = Physics2D.Raycast(collPoint.position, direction, rangeDetect, playerLayer);

        if(hitPlayer.collider != null)
        {

            currentSpeed = 0;

            anim.SetTrigger("attack");
                
            OnAttack();

        }
        else 
        {

            currentSpeed = speed;

            anim.SetTrigger("run");
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



    void OnAttack()
    {
        if(nextFireTime < Time.time)
        {
            Instantiate(bullet, collPoint.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
            //StartCoroutine(AnimationShoot());
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



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 formDirection = collPoint.TransformDirection(direction) * rangeDetect;
        Gizmos.DrawRay(collPoint.position, formDirection);
    }
}
