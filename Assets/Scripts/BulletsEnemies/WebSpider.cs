using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSpider : MonoBehaviour
{

    private Player player;

    public float timeStopped;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        
        StartCoroutine(StoppedPlayer());

        //Destroy(gameObject, timeStopped);
    }


    IEnumerator StoppedPlayer()
    {
        player.isStopped = true;
        
        player.rig.bodyType = RigidbodyType2D.Static;

        player.anim.SetInteger("transition", 0);


        yield return new WaitForSeconds(timeStopped);

        player.isStopped = false;

        player.rig.bodyType = RigidbodyType2D.Dynamic;

        Destroy(gameObject);
        
        
    }

    
}
