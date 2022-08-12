using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class InteractObjects : MonoBehaviour
{
    public GameObject arrowDownBed;

    [Header("Stats")]

    public float pushArea;
    public LayerMask playerLayer;


    void Update()
    {
        Collider2D hasCollider = Physics2D.OverlapCircle(transform.position, pushArea, playerLayer);

        if(hasCollider != null && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.CompareTag("ToSleep"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
            }
            else if (gameObject.CompareTag("ToComputer"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
            
        }
    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 8)
        {
            arrowDownBed.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            arrowDownBed.SetActive(false);
        }
    }
}
