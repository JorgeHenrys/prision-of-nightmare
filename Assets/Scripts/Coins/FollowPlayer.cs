using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    private Transform target;
    private GameController gameController;

    public float minModifier;
    public float maxModifier;

    Vector3 _velocity = Vector3.zero;
    bool _isFollowing = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        gameController = FindObjectOfType<GameController>();
    }

    public void StartFollowing() 
    {
        _isFollowing = true;
    }


    void Update()
    {

        if (_isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref _velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
        }
        
    }


    void OnTriggerEnter2D(Collider2D collider)
    {   
        if (collider.gameObject.layer == 8)
        {
            gameController.AddCoin();
            Destroy(gameObject);
        }
    }
}
