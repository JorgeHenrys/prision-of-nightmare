using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtomic : MonoBehaviour
{

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator anim;
    public Transform collPoint;
    public BoxCollider2D collision2D;
    private Player player;
    public GameObject coinLoot;

    private GameController gameController;



    [Header("Stats")]
    public float speed;

    private float currentSpeed;
    public float attackSpeed;
    public float rangeDetect;
    public bool modeZumbi = false;

    public float health;
    public float maxHealth;
    public HealthBarEnemy healthBar;
    public GameObject deathEffect;
    


    [Header("Hit Settings")]
    private bool isRight;
    private Vector2 direction;
    public float headArea;
    public LayerMask playerLayer;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.totalEnemies += 1;

        currentSpeed = speed;
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
        player = FindObjectOfType<Player>();
    }


    void Update()
    {
        DetectPlayer();
        if (isRight)
        {
            direction = Vector2.right;
            transform.eulerAngles = new Vector2(0, 180);
        }
        else
        {
            direction = -Vector2.right;
            transform.eulerAngles = new Vector2(0, 0);
        }
    }

    void FixedUpdate()
    {
        rig.MovePosition(rig.position + direction * currentSpeed * Time.deltaTime);
    }


    void DetectPlayer() 
    {
        var diferenceForPlayerX = player.gameObject.transform.position.x - transform.position.x;
        var followPlayerX = Mathf.Abs(diferenceForPlayerX) < rangeDetect;

        RaycastHit2D hitPlayerLeft = Physics2D.Raycast(collPoint.position, Vector2.left, rangeDetect, playerLayer);
        RaycastHit2D hitPlayerRight = Physics2D.Raycast(collPoint.position, Vector2.right, rangeDetect, playerLayer);

        if(modeZumbi)
        {
            if(hitPlayerLeft.collider != null || hitPlayerRight.collider != null)
            {

                currentSpeed = attackSpeed;
                

                if(diferenceForPlayerX < 0)
                {
                    isRight = false;
                }
                else 
                {
                    isRight = true;
                }

            }
            else 
            {
                currentSpeed = speed;
            }
        }
        
        
    }


    void OnAttack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(collPoint.position, headArea, playerLayer);

       

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

            gameController.totalEnemies -= 1;

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
        if (collision.gameObject.layer == 8)
        {
            currentSpeed = 0f;
            collision2D.enabled = false;
            anim.SetTrigger("attack");
            
            OnAttack();
            Destroy(gameObject, 0.8f);
            
        }
    }
}
