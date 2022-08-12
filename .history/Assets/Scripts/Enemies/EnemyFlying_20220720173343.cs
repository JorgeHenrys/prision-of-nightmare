using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlying : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;
    public float health;
    public float maxHealth;


    public GameObject bullet;
    public GameObject bulletParent;
    public Animator anim;
    private Transform player;
    private NavMeshAgent agent;
    public LayerMask groundLayer;
    private Vector2 playerVector;
    public HealthBarEnemy healthBar;
    public GameObject deathEffect;
    public GameObject coinLoot;

    private GameController gameController;


    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.totalEnemies += 1;

        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        playerVector = player.position - transform.position;
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        RaycastHit2D hasObstacle = Physics2D.Raycast(transform.position, playerVector, shootingRange, groundLayer);

        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime); 
            //agent.SetDestination(player.position);
        }
        else if (distanceFromPlayer < shootingRange && nextFireTime < Time.time && hasObstacle.collider == null)
        {
            anim.SetTrigger("shooting");
            
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
            StartCoroutine(AnimationShoot());
        }


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

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }


    private void OnDrawGizmosSelected()
    {
        //Vector2 formPlayer = transform.TransformDirection(playerVector) * shootingRange;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position, formPlayer);
    }

    IEnumerator AnimationShoot()
    {

        yield return new WaitForSeconds(0.8f);

        anim.SetTrigger("flying");
    }
}
