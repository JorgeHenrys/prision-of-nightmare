using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingPlayer : MonoBehaviour
{
    public Image fallingScreen;

    void Start()
    {
        StartCoroutine(TimeEndFalling());
    }

    IEnumerator TimeEndFalling()
    {

        yield return new WaitForSeconds(6f);

        fallingScreen.gameObject.SetActive(false);

    }

}
