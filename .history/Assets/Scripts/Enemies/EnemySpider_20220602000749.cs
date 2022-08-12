using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : MonoBehaviour
{

    
    public Transform groundPoint;
    public LayerMask groundLayer;
    public Rigidbody2D rig;
    public Animator anim;
    public HealthBarEnemy healthBar;
    public GameObject deathEffect;
    public GameObject coinLoot;


    private Player player;


    public float health;
    public float maxHealth;
    public float speed;
    public float rayDetectGround;
    public float jumpForce;
    private Vector2 directionRay;
    private int hasDead;
    
    void Start()
    {
        hasDead = 0;
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
        player = FindObjectOfType<Player>();
    }

    
    void Update()
    {
        FollowPlayer();
        Flip();
        JumpObstacle();
    }


    private void FollowPlayer() 
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.transform.position) <= 1.5f)
        {

            if(!player.hasDashing && hasDead == 0)
            {
                hasDead = 1;
                speed = 0f;
                anim.SetTrigger("destroy");

                player.isVulnerable = false;
                    
                player.GetHit(0.5f, 2f);
                    
                StartCoroutine(Death(0.7f));
            }
                
            
            
        }
    }



    private void JumpObstacle()
    {
        RaycastHit2D hasObstacle = Physics2D.Raycast(groundPoint.position, directionRay, rayDetectGround, groundLayer);
        if (hasObstacle.collider != null)
        {
            rig.velocity = Vector2.up * jumpForce;
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



    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            directionRay = Vector2.left;
            
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            directionRay = Vector2.right;
            
            
        }
    }

    IEnumerator Death(float t)
    {
        

        yield return new WaitForSeconds(t);

        Destroy(gameObject);
    }
}
