using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawEnemies : MonoBehaviour
{
   
    public GameObject[] enemies;
    


    void Start()
    {
        int rand = Random.Range(0, enemies.Length);
        GameObject instance = (GameObject)Instantiate(enemies[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }

}

