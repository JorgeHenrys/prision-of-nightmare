using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject selectedGun;
    public GameObject gun;
    private Sprite gunSprite;
    

    private Player player;

    public Sprite[] spritesPlayerHud;


    public Image lifeBar;
    public Image stamineBar;
    public Image healthGunBar;

    public Image playerHud;
    public Text coinText;
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public int totalEnemies = 0;
    public bool enableSpawEnemy;
    
    private int coin;
    private bool hasUsingWeapon = false;

    void Start()
    {
        

        if (SceneManager.GetActiveScene().name == "RoomBob")
        {
            player = FindObjectOfType<Player>();
        }
        else 
        {
            player = FindObjectOfType<Player>();

            coin = 0;
            coinText.text = "00" + coin.ToString();
            gunSprite = selectedGun.GetComponent<SpriteRenderer>().sprite;
            gun.GetComponent<SpriteRenderer>().sprite = gunSprite;
            playerHud.sprite = spritesPlayerHud[0];
        }
         
    }

    void Update()
    {   
        if(player.GetComponent<Player>().health <= 5f)
        {
            playerHud.sprite = spritesPlayerHud[1];
        }
    }

    public void GameOver()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }

        
    }


    public void GetHit(float damage)
    {
        if (lifeBar.fillAmount > 0)
        {

            
            lifeBar.fillAmount -= 0.1f * damage;

        }
        
    }

    public void UseEstamine()
    {
        if (stamineBar.fillAmount >= 0)
        {
            stamineBar.fillAmount -= 0.5f;
            StartCoroutine(UpdateStamine());
        }
    }


    public void UseHealthWeapon()
    {
        if (healthGunBar.fillAmount >= 0)
        {
            healthGunBar.fillAmount -= 0.1f;

            if(hasUsingWeapon == false)
            {
                hasUsingWeapon = true;
                StartCoroutine(UpdateHeathGun());
            }
            
        }
    }


    public void AddCoin()
    {
        coin += 1;

        if (coin / 10f < 1f)
        {
            coinText.text = "00" + coin.ToString();
        }
        else if (coin / 100f < 1f)
        {
            coinText.text = "0" + coin.ToString();
        }
        else
        {
            coinText.text = coin.ToString();
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("RoomBob");
    }

    IEnumerator UpdateStamine()
    {
        yield return new WaitForSeconds(5f);

        while(stamineBar.fillAmount < 1f)
        {
            player.GetComponent<Player>().stamine += 1;
            stamineBar.fillAmount += 0.1f;
            yield return new WaitForSeconds(5f);
        }
        
    }


    IEnumerator UpdateHeathGun()
    {
        yield return new WaitForSeconds(4f);

        while(healthGunBar.fillAmount < 1f)
        {
            healthGunBar.fillAmount += 0.05f;
            yield return new WaitForSeconds(1.5f);
        }

        if (healthGunBar.fillAmount >= 1f)
        {
            hasUsingWeapon = false;
        }
        
    }

    
}
