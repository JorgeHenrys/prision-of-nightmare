using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFish : MonoBehaviour
{
    public Rigidbody2D rig;
    public Transform headPoint;
    private Player player;
    public HealthBarEnemy healthBar;
    public GameObject deathEffect;
    public GameObject coinLoot;
    
    private Vector2 direction;
    

    public LayerMask acideLayer;

    public float health;
    public float maxHealth;
    public float speed;
    public float rayDetectOutSide;

    private float currentSpeed;
    private bool isRight = true;
    private bool isAttacking = false;
    
    void Start()
    {
        currentSpeed = speed;
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
        player = FindObjectOfType<Player>();
    }

    
    void Update()
    {
        DetectAreaSwim();
        DetectPlayer();
        if (!isAttacking)
        {
            Flip();
        } 
        else
        {
            FlipToPlayer();
        }
        
    }


    void FixedUpdate()
    {
        rig.MovePosition(rig.position + direction * currentSpeed * Time.deltaTime);
    }


    private void Flip()
    {
        if (isRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
            direction = Vector2.right;
            
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            
            direction = Vector2.left;
            
            
        }
    }



    private void FlipToPlayer()
    {
        if (player.transform.position.x > transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }


    private void DetectAreaSwim()
    {
        RaycastHit2D hasOutSide = Physics2D.Raycast(headPoint.position, direction, rayDetectOutSide, acideLayer);
        if (hasOutSide.collider == null)
        {
            isRight = !isRight;
        }

    }



    private void DetectPlayer()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= 8f && player.transform.position.y > transform.position.y)
        {
            
             
            StartCoroutine(AttackPlayer(3f));                       
            
        }
    }

    void OnAttack()
    {
        
        if (player.isVulnerable)
        {
            
            if(!player.roll)
            {
                player.isVulnerable = false;
                player.GetHit(0.5f, 2f);
            }
            
           
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
        if (collision.gameObject.layer == 8)
        {
            OnAttack();
            Destroy(gameObject);
            
        }
    }



    IEnumerator AttackPlayer(float t)
    {

        yield return new WaitForSeconds(t);

        isAttacking = true;

        Vector3 posPlayer = player.transform.position;

        transform.position = Vector2.MoveTowards(transform.position, posPlayer, currentSpeed * 2 * Time.deltaTime);
        
        yield return new WaitForSeconds(2.5f);

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

}
