using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticleShoot : MonoBehaviour
{

    private float randSize;


    void Start()
    {
        randSize = Random.Range(1f, 2f);
    }

    void Update()
    {
        
        if (transform.localScale.x < randSize)
        {
            transform.localScale += new Vector3(0.05f, 0.05f, 0);
        }
        else {
            Destroy(gameObject);
        }
    }
}
