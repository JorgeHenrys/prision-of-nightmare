using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawEnemies : MonoBehaviour
{
   
    public GameObject[] enemies;

    private GameController gameController;
    


    void Start()
    {
        gameController = FindObjectOfType<GameController>();


        if(gameController.totalEnemies <= 6 && gameController.enableSpawEnemy)
        {
            int rand = Random.Range(0, enemies.Length);
            GameObject instance = (GameObject)Instantiate(enemies[rand], transform.position, Quaternion.identity);
            gameController.totalEnemies += 1;
            instance.transform.parent = transform;
        }

        
    }

}

