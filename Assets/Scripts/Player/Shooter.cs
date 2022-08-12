using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class Shooter : MonoBehaviour
{
    public GameObject buletMachine;
    public GameObject buletShotgun;
    public GameObject buletPlasm;
    public GameObject selectedGun;
    private string gunSprite;
    public Transform startOne;
    public Transform startTwo;
    public Transform startThree;
    public Light2D lightShooter;

    private Player player;
    private GameController gameController;

    private float timeBtwShots;
    private float startTimeBtwShots;
   

    void Start()
    {
        gunSprite = selectedGun.GetComponent<SpriteRenderer>().sprite.name;
        gameController = FindObjectOfType<GameController>();
        player = FindObjectOfType<Player>();
        
    }
    void Update()
    {
        Vector3 gunpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(gunpos.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
        }
        else 
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
        }

        if (timeBtwShots <= 0 ) 
        {

            if (player.isStopped == false && SceneManager.GetActiveScene().name != "RoomBob" && gameController.healthGunBar.fillAmount > 0.2f)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(LightEffectShooter());
                    gameController.UseHealthWeapon();

                    if(gunSprite == "gun_shotgun"){
                        Shooting(startOne, buletShotgun);
                        Shooting(startTwo, buletShotgun);
                        Shooting(startThree, buletShotgun);
                        timeBtwShots = startTimeBtwShots;
                    }
                    else if(gunSprite == "gun_machine"){
                        Shooting(startThree, buletMachine);
                        timeBtwShots = startTimeBtwShots;
                    }
                    else if(gunSprite == "gun_plasm"){
                        Shooting(startThree, buletPlasm);
                        timeBtwShots = startTimeBtwShots;
                    }
                    
                }
            }

            
            
        } 
        else 
        {
            timeBtwShots -= Time.deltaTime;
        }

        
    }

    void Shooting(Transform init, GameObject bulet)
    {
        
        GameObject shoot = Instantiate(bulet, init.transform.position, init.transform.rotation);
        
    }


    IEnumerator LightEffectShooter()
    {

        lightShooter.intensity = 3f;

        yield return new WaitForSeconds(0.1f);

        lightShooter.intensity = 1f;
    }

    
}
