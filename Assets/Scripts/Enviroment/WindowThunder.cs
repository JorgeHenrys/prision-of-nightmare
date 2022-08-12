using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WindowThunder : MonoBehaviour
{

    public Light2D lightWindow;

    private bool hasThunded = false;


    void Start()
    {
       //lightWindow = GetComponent<Light2D>();
    }


    void Update()
    {
        if (hasThunded == false)
        {
            hasThunded = true;
            StartCoroutine(Thunder());
        }
    }


    IEnumerator Thunder()
    {
        float waitTime = Random.Range(8f, 15f);

        yield return new WaitForSeconds(waitTime);

        //Debug.Log(lightWindow.intensity);

        lightWindow.intensity = 2f;

        yield return new WaitForSeconds(0.5f);

        lightWindow.intensity = 1f;

        hasThunded = false;
    }
    
}
