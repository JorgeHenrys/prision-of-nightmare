using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInRoom : MonoBehaviour
{

    public Animator anim;
    
    void Start()
    {

        int rand = Random.Range(0, 2);

        anim.SetInteger("transition", rand);
        
    }

}
