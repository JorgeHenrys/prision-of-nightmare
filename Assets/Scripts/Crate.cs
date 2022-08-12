using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{

    [Header("Components")]
    private Player player;
    public BoxCollider2D collision2D;
    public Rigidbody2D rig;
    public Transform crouchedPoint;

    public Transform push;
    
   
    [Header("Stats")]
    public float crouchedArea;

    public float pushArea;
    public LayerMask playerLayer;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }


    void Update()
    {
        OnPush();

        
    }

    

    void OnPush()
    {
        Collider2D hasPushed = Physics2D.OverlapCircle(push.position, pushArea, playerLayer);

        
        if(hasPushed != null)
        {
            hasPushed.GetComponent<Player>().OnPushed();
            rig.isKinematic = false;
        }
        else 
        {
            
            rig.isKinematic = true;
        }
        
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(push.position, pushArea);


    }
    
        
}
