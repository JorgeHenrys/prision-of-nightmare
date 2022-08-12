using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWorm : MonoBehaviour
{
    public Animator anim;
    public HealthBarEnemy healthBar;
    public GameObject deathEffect;
    public GameObject coinLoot;

    private SpawWorms spawWorms;


    public float health;
    public float maxHealth;

    void Start()
    {
        spawWorms = FindObjectOfType<SpawWorms>();
        StartCoroutine(Started(1f));

        if(spawWorms.damageWorm == 0)
        {
            health = maxHealth;
        }
        else 
        {
            health = spawWorms.damageWorm;
        }
        
        healthBar.SetHealth(health, maxHealth);
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health, maxHealth);
        spawWorms.GetHeath(damage);
        spawWorms.damageWorm = health;

        if (health <= 0) 
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);

            Instantiate(coinLoot, transform.position, Quaternion.identity);

            spawWorms.damageWorm = 0;
            spawWorms.countEnemies -= 1;

            Destroy(gameObject);
        
        }
    }


    



    IEnumerator Started(float t)
    {     

        yield return new WaitForSeconds(t);

        anim.SetTrigger("starting");

        yield return new WaitForSeconds(2.5f);

        spawWorms.damageWorm = health;

        if(health <= 0)
        {
            spawWorms.countEnemies -= 1;
        }

        Destroy(gameObject);
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            spawWorms.OnAttack();
            
        }
    }

}
