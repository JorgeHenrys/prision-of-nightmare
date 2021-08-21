using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBirdman : MonoBehaviour
{

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator anim;
    public Transform collPoint;
    public BoxCollider2D collision2D;
    public GameObject target;
    public GameObject powerPlayer;
    public SpriteRenderer spriteRenderer;
    


    [Header("Stats")]
    public float speed;
    public float rangeDetect;
    public bool modeZumbi = false;
    public Player player;



    [Header("Hit Settings")]
    private bool isRight;
    private Vector2 direction;
    public float headArea;
    public LayerMask playerLayer;

    void Start()
    {

    }


    void Update()
    {
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

        DetectEnemy();
    }

    void FixedUpdate()
    {
        rig.MovePosition(rig.position + direction * speed * Time.deltaTime);

    }

    void DetectEnemy()
    {
        var diferenceForEnemyBird = player.gameObject.transform.position.x - transform.position.x;
        var detectedEnemyBird = Mathf.Abs(diferenceForEnemyBird) < rangeDetect;
        
        

        if(detectedEnemyBird && Input.GetKey(KeyCode.L))
        {  
            target.SetActive(true);

            if(Input.GetKey(KeyCode.K))
            {

                powerPlayer.SetActive(true);
                spriteRenderer.enabled = false;
                target.SetActive(false);
                
                Destroy(gameObject, 1.2f);
            }
        }
        else
        {
            target.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checa se está colidindo com algum obstáculo
        if (collision.gameObject.layer == 10)
        {
            isRight = !isRight;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeDetect);
    }
}
