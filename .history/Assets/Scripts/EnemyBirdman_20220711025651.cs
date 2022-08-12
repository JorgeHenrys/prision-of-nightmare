using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBirdman : MonoBehaviour
{

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator anim;
    public Transform attackPoint;
    public Transform centerPoint;
    public BoxCollider2D collision2D;
    public GameObject target;
    public GameObject powerPlayer;
    public SpriteRenderer spriteRenderer;
    public GameObject coinLoot;
    


    [Header("Stats")]
    public float speed;
    public float rangeDetect;
    public bool modeZumbi = false;
    private float speedCurrent;
    private float speedCurrentPlayer;
    public float health;
    public float maxHealth;

    private Player player;
    



    [Header("Hit Settings")]
    private bool isRight;
    private Vector2 direction;

    private Vector2 directionAttack;
    public float attackRay;
    public LayerMask playerLayer;
    public float throwPlayerForce;
    public GameObject deathEffect;
    public HealthBarEnemy healthBar;


    void Start()
    {
        speedCurrent = speed;
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
        player = FindObjectOfType<Player>();
    }


    void Update()
    {


        if (isRight)
        {
            direction = Vector2.right;
            directionAttack = Vector2.right;
            transform.eulerAngles = new Vector2(0, 180);
            Debug.Log("DIREITA");
        }
        else
        {   
            directionAttack = Vector2.left;
            direction = -Vector2.right;
            transform.eulerAngles = new Vector2(0, 0);
            Debug.Log("Esquerda");
        }


        
    }

    void FixedUpdate()
    {
        rig.MovePosition(rig.position + direction * speed * Time.deltaTime);
        OnHit();
        DetectPlayer();

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

    void OnHit() 
    {
        RaycastHit2D onHitPlayer = Physics2D.Raycast(centerPoint.position, directionAttack, attackRay, playerLayer);

        if (onHitPlayer.collider != null && player.isVulnerable && !player.hasDashing)
        {


            speedCurrent = speed;
            speedCurrentPlayer = player.GetComponent<Player>().speed;

            speed = 0f;
            player.GetComponent<Player>().speed = 0f;
            anim.SetTrigger("attack");
            
            
            StartCoroutine(DelayAction());

            
        }

       
        
    }

    void DetectPlayer()
    {
        var diferenceForEnemyBird = player.gameObject.transform.position.x - transform.position.x;
        var followPlayer = Mathf.Abs(diferenceForEnemyBird) < rangeDetect;
        

        RaycastHit2D hitPlayerLeft = Physics2D.Raycast(centerPoint.position, Vector2.left, rangeDetect, playerLayer);
        RaycastHit2D hitPlayerRight = Physics2D.Raycast(centerPoint.position, Vector2.right, rangeDetect, playerLayer);
        

        if(modeZumbi && player.isVulnerable)
        {
            if(hitPlayerLeft.collider != null || hitPlayerRight.collider != null)
            {
                
                if(diferenceForEnemyBird < 0)
                {
                    isRight = false;
                }
                else 
                {
                    isRight = true;
                }

            }
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
        Gizmos.color = Color.green;
        Vector2 formLeft = centerPoint.TransformDirection(Vector2.left) * rangeDetect;
        Vector2 formRight = centerPoint.TransformDirection(Vector2.right) * rangeDetect;
        Vector2 formAttack = centerPoint.TransformDirection(directionAttack) * attackRay;
        Gizmos.DrawRay(centerPoint.position, formLeft);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(centerPoint.position, formRight);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(centerPoint.position, formAttack);
    }

    float timeInterval = 0.85f;
    IEnumerator DelayAction()
    {
        
        player.GetComponent<Player>().speed = speedCurrentPlayer;

        player.isVulnerable = false;

        yield return new WaitForSeconds(timeInterval);
        
        player.transform.position = Vector2.MoveTowards(powerPlayer.transform.position, target.transform.position, 40f * Time.deltaTime);

        

        
        speed = speedCurrent;
        
        
        player.GetHit(0.5f, 2f);
    }
}
