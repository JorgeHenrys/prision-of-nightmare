using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFollowPlayer : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;

    private Transform target;
    public Animator anim;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        int numberTransition = Random.Range(0, 4);   

        anim.SetInteger("transition", numberTransition); 
    }

    void Update()
    {
        
        if ( Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position + new Vector3(0f, 2f, 0f), speed * Time.deltaTime);
        }

        if(target.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
    }
}
