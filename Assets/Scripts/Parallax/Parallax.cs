using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cameraPlayer;
    private float lengthX, startPosX, lengthY, startPosY;
    public float speedParallax;


    void Start()
    {
        startPosX = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;

        startPosY = transform.position.y;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;

    }

   
    void FixedUpdate()
    {
        float tempX = (cameraPlayer.transform.position.x * (1 - speedParallax));
        float distX = (cameraPlayer.transform.position.x * speedParallax);

        float tempY = (cameraPlayer.transform.position.y * (1 - 0.6f));
        float distY = (cameraPlayer.transform.position.y * 0.6f);


        
        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        //PARALLAX NO X
        if (tempX > startPosX + lengthX / 2) 
        {
            startPosX += lengthX;
        }
        else if (tempX < startPosX - lengthX / 2)
        {
            startPosX -= lengthX;
        }


        //PARALLAX NO Y

        if (tempY > startPosY + lengthY) 
        {
            startPosY += lengthY;
        }
        else if (tempY < startPosY - lengthY)
        {
            startPosY -= lengthY;
        }
    }
}
