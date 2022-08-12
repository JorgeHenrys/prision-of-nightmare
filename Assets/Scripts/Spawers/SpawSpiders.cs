using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawSpiders : MonoBehaviour
{
    
    public GameObject enemySpider;

    private Player player;


    private bool hasNextTime;
    
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        hasNextTime = false;
    }       

    
    void Update()
    {
        int amountSpiders = GameObject.FindGameObjectsWithTag("EnemySpider").Length;


        if (hasNextTime == false && amountSpiders <= 3) 
        {
            hasNextTime = true;
            
            StartCoroutine(Respawn(4.5f));
        }

        
        
    }


    IEnumerator Respawn(float t)
    {

        Vector3 playerPos = player.transform.position;

        yield return new WaitForSeconds(t);

        Instantiate(enemySpider, new Vector3(playerPos.x, transform.position.y,  0), Quaternion.identity);

        hasNextTime = false;
        
    }
}
