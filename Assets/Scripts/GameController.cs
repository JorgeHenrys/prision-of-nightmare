using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Image lifeBar;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    // public void adicionarMoedas()
    // {
    //     totalCoins++;
    //     coinText.text = totalCoins.ToString();
    // }

    public void TakeDamage(float value) 
    {
        lifeBar.fillAmount = value/10;
    }
}
