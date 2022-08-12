using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawTiles : MonoBehaviour
{
    public GameObject[] tileLvL1;
    public GameObject[] tileLvL2;

    private GameController gameController;

    
    void Start()
    { 

        gameController = FindObjectOfType<GameController>();

        if(gameController.currentLevel  <= 2)
        {
            int rand = Random.Range(0, tileLvL1.Length);
            GameObject instance = (GameObject)Instantiate(tileLvL1[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
        else if(gameController.currentLevel  > 2 && gameController.currentLevel  <= 4)
        {
            int rand = Random.Range(0, tileLvL2.Length);
            GameObject instance = (GameObject)Instantiate(tileLvL2[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }  
        
        
        
    }
}
