using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawWorms : MonoBehaviour
{

    private Player player;
    public GameObject enemyWorm;



    public float damageWorm;
    public int countEnemies = 1;

    private bool isDown;
    
    
    void Start()
    {
        
        isDown = false;

        player = FindObjectOfType<Player>();
    }       

    
    void Update()
    {
        if (isDown == false && player.isGrounded && countEnemies > 0) 
        {
            isDown = true;
            
            StartCoroutine(Respawn(4.5f));
        }

        
        
    }

    public void GetHeath(float damage)
    {
        damageWorm = damage;  
    }


    public void OnAttack()
    {

        if (player.isVulnerable)
        {
            
            if(!player.roll)
            {
                player.isVulnerable = false;
                player.GetHit(0.5f, 2f);
            }
            
           
        }
        
    }



    IEnumerator Respawn(float t)
    {

        Vector3 playerPos = player.transform.position;

        yield return new WaitForSeconds(t);

        Instantiate(enemyWorm, playerPos + new Vector3(0, 1f,  0), Quaternion.identity);
        isDown = false;
        
    }
}
