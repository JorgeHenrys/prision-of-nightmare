using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingPlayer : MonoBehaviour
{
    public Image fallingScreen;

    private Player player;

    void Start()
    {
        StartCoroutine(TimeEndFalling());
        player = FindObjectOfType<Player>();
    }

    IEnumerator TimeEndFalling()
    {

        yield return new WaitForSeconds(6f);

        fallingScreen.gameObject.SetActive(false);
        player.gameObject.SetActive(true);

    }

}
