using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

    [Header("Components")]
    public Player player;
    public Transform crouchedPoint;

    
   
    [Header("Stats")]
    public float crouchedArea;

    public LayerMask playerLayer;
    void Start()
    {
        
    }

    void Update()
    {
        //OnCrouched();
    }

    void OnCrouched()
    {
        Collider2D hasCrouched = Physics2D.OverlapCircle(crouchedPoint.position, crouchedArea, playerLayer);

         if (Input.GetKey(KeyCode.M) && hasCrouched != null)
        {
            hasCrouched.GetComponent<Player>().isCrouched = true;
            
        }
        else
        {
            hasCrouched.GetComponent<Player>().isCrouched = false;
        }
        
        
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(crouchedPoint.position, crouchedArea);

    }
}
