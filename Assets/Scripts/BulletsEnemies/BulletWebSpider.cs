using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWebSpider : MonoBehaviour
{

    public GameObject web;

    GameObject target;
    Rigidbody2D bulletRB;

    public float speed;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);

        Destroy(this.gameObject, 2.5f);
    }

    void Update()
    {
        if (transform.localScale.x < 2f)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0);
        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Instantiate(web, target.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    
}
