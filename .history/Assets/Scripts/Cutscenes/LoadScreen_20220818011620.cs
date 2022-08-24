using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    public Image skullImage;
    void Start()
    {
        StartCoroutine(FadeImage(true));
    }

     
   
 
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                skullImage.color = new Color(1, 1, 1, i);

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            StartCoroutine(FadeImage(false));
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                skullImage.color = new Color(1, 1, 1, i);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            StartCoroutine(FadeImage(true));
        }
    }
}
