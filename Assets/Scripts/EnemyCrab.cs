using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrab : MonoBehaviour
{

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator anim;
    public Transform collPoint;
    public BoxCollider2D collision2D;
    public Player player;

    [Header("Stats")]
    public float speed;
    public float rangeDetect;
    public bool modeZumbi = false;
    


    [Header("Hit Settings")]
    private bool isRight;
    private Vector2 direction;
    public float headArea;
    public LayerMask playerLayer;
    private bool playerFirstDetected = false;
    private bool playerLastDetected = false;
    
    
    void Update()
    {

        DetectPlayer();
        
        if(playerFirstDetected)
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
        }
        

    }

    void DetectPlayer() 
    {
        var diferenceForPlayer = player.gameObject.transform.position.x - transform.position.x;
        var followPlayer = Mathf.Abs(diferenceForPlayer) < rangeDetect;

        if(modeZumbi && followPlayer)
        {
            if(playerFirstDetected)
            {
                playerLastDetected = true;
            }
            else 
            {
                playerFirstDetected = true;
            }
            
            
            

            if(diferenceForPlayer < 0)
            {
                isRight = false;
            }
            else 
            {
                isRight = true;
            }

            StartCoroutine(PlayAnimationAwakening());
        }
        
    }

    void OnAttack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(collPoint.position, headArea, playerLayer);

         if (hitPlayer != null)
        {
            
            if(playerLastDetected && !hitPlayer.GetComponent<Player>().roll)
            {
                hitPlayer.GetComponent<Player>().GetHit();
            }
            
           
        }
        
    }

    void FixedUpdate()
    {
        rig.MovePosition(rig.position + direction * speed * Time.deltaTime);

        OnAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checa se está colidindo com algum obstáculo
        if (collision.gameObject.layer == 10)
        {
            isRight = !isRight;
        }
        if (collision.gameObject.layer == 8)
        {
            if(playerLastDetected)
            {
                collision2D.enabled = false;
                anim.SetTrigger("charge");
                speed = 0f;
                Destroy(gameObject, 1.2f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeDetect);
    }

    public IEnumerator PlayAnimationAwakening() {

        anim.SetTrigger("awakening");

        yield return new WaitForSeconds(1);

        anim.SetTrigger("run");
    }
}
