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

        Debug.Log(gameController.enableSpawEnemy);

        if(gameController.totalEnemies <= 6 && gameController.enableSpawEnemy)
        {
            int rand = Random.Range(0, enemies.Length);
            GameObject instance = (GameObject)Instantiate(enemies[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }

        
    }

}

