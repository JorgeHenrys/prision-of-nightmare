using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class GoDreams : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PlayStartDream());
        
    }

    public IEnumerator PlayStartDream() {

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
