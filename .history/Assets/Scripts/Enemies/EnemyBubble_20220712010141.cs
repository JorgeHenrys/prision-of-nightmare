using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBubble : MonoBehaviour
{

    public Transform startingPoint;
    public HealthBarEnemy healthBar;
    public GameObject deathEffect;
    public GameObject coinLoot;


    public float health;
    public float maxHealth;
    public float speed;
    public bool chase = false;
    


    private Player player;

    void Start()
    {
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player == null)
            return;
        if (chase == true && player.isVulnerable)
            Chase();
        else
            ReturnStartPoint();
        Flip();
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



    private void Chase() 
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
        {
             
                if(!player.roll)
                {
                    player.isVulnerable = false;
                    
                    player.GetHit(0.5f, 2f);
                }
                
            
            
        }
    }


    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }


    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
