using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticleShoot : MonoBehaviour
{


    void Update()
    {
        if (transform.localScale.x < 2f)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0);
        }
    }
}
