using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpider : MonoBehaviour
{
    public Animator anim;
    public Transform[] movePositions;
    public GameObject web;
    public Transform webPoint;
    public Image bossLifeBar;
    public GameObject coinLoot;

    private Transform currentMovePosition;
    private Player player;


    public float jumpForce;
    public float fireRate = 1f;
    public float health;
    

    private Vector3 direction;
    private float currentJumpForce;
    private bool hasJumping = false;
    private int currentPos;
    private int currentPosUp;
    private float nextFireTime;


    void Start()
    {
        currentJumpForce = 0;
        currentPos = 0;

        player = FindObjectOfType<Player>();
        
    }

    void Update()
    {
        Flip();
        if (player.isStopped == false && nextFireTime < Time.time)
        {
            ShootWeb();
        }
        
        if (currentJumpForce != 0)
        {
            Jump();
        }

        if (hasJumping == false)
        { 
            hasJumping = true;


            StartCoroutine(ChangePosition());
        }
    }


    void Jump()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentMovePosition.position, currentJumpForce * Time.deltaTime);
    }

    void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            direction = Vector2.left;
            
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            direction = Vector2.right;
            
            
        }
    }

    void ShootWeb() 
    {
        Instantiate(web, webPoint.position, Quaternion.identity);
        nextFireTime = Time.time + fireRate;
    }


    void RandomChangePos()
    {

        int rand = Random.Range(1, 3);

        if (currentPos == 0)
        {
            currentPos = 2;
            currentPosUp = 1;
        }

        else if (currentPos == 2)
        {
            if(rand == 1)
            {
                currentPos = 0;
                currentPosUp = 1;
            }
            else 
            {
                currentPos = 4;
                currentPosUp = 3;
            }
        }

        else if (currentPos == 4)
        {
            if(rand == 1)
            {
                currentPos = 2;
                currentPosUp = 3;
            }
            else 
            {
                currentPos = 6;
                currentPosUp = 5;
            }
        }

        else if (currentPos == 6)
        {
            currentPos = 4;
            currentPosUp = 5;
        }
    }



    public void TakeDamage(float damage)
    {
        if (bossLifeBar.fillAmount > 0)
        {
            bossLifeBar.fillAmount -= 0.01f * damage;

        }
        else 
        {
            Instantiate(coinLoot, transform.position, Quaternion.identity);
            Instantiate(coinLoot, transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
            Instantiate(coinLoot, transform.position + new Vector3(1f, 0, 0), Quaternion.identity);

            Destroy(gameObject, 1.5f);
        }
        
    }



    IEnumerator ChangePosition()
    {
        RandomChangePos();


        float timeWait = Random.Range(3.5f, 5.5f);


        yield return new WaitForSeconds(timeWait);

        anim.SetTrigger("jump");

        yield return new WaitForSeconds(1f);

        anim.SetTrigger("idle");

        currentMovePosition = movePositions[currentPosUp];
        currentJumpForce = jumpForce;

        yield return new WaitForSeconds(0.5f);

        currentMovePosition = movePositions[currentPos];

        yield return new WaitForSeconds(2f);

        currentJumpForce = 0;
        hasJumping = false;

        
        //yield return new WaitForSeconds(2f);

        
    }
}
