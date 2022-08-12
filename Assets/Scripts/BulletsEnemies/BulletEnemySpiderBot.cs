using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemySpiderBot : MonoBehaviour
{

    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);

        Destroy(this.gameObject, 2f);
    }

    void Hit(Collider2D collision) 
    {
        if (collision.GetComponent<Player>().isVulnerable)
        {
                
            if(!collision.GetComponent<Player>().hasDashing)
            {
                collision.GetComponent<Player>().isVulnerable = false;
                collision.GetComponent<Player>().GetHit(0.5f, 2f);
            }
                
            
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            
            Hit(collision);
            Destroy(gameObject);
        }
    }

    
}
