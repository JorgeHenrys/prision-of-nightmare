using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{

    private Player player;
    

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 gunPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - gunPosition.x;
        mousePos.y = mousePos.y - gunPosition.y;

        float gunAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (player.isStopped == false)
        {
            if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));
            }
        }

        
    }
}
