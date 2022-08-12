using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    public GameObject destroyEffect;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        
    }
    
    void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {

            //LINE ENEMY
            if (hitInfo.collider.CompareTag("EnemyBirdman"))
            {
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                hitInfo.collider.GetComponent<EnemyBirdman>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("EnemyCrab"))
            {
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                hitInfo.collider.GetComponent<EnemyCrab>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("EnemyAtomic"))
            {
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                hitInfo.collider.GetComponent<EnemyAtomic>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("EnemyBubble"))
            {
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                hitInfo.collider.GetComponent<EnemyBubble>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("EnemySpider"))
            {
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                hitInfo.collider.GetComponent<EnemySpider>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("EnemyFlying"))
            {
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                hitInfo.collider.GetComponent<EnemyFlying>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("EnemySpiderBot"))
            {
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                hitInfo.collider.GetComponent<EnemySpiderBot>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("EnemyWheelBot"))
            {
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                hitInfo.collider.GetComponent<EnemyWheelBot>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("EnemyWorm"))
            {
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                hitInfo.collider.GetComponent<EnemyWorm>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("EnemyFish"))
            {
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                hitInfo.collider.GetComponent<EnemyFish>().TakeDamage(damage);
            }


            //LINE BOSS
            if (hitInfo.collider.CompareTag("BossSpider"))
            {
                CinemachineShake.Instance.ShakeCamera(5f, .1f);
                hitInfo.collider.GetComponent<BossSpider>().TakeDamage(damage);
            }

            DestroyProjectile();
        }


        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyProjectile() 
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
