using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePositionCamPortal : MonoBehaviour
{

    public GameObject portal;

    void Update()
    {
        transform.position = portal.transform.position + new Vector3(0f, 5f, 0f);
        Debug.Log(transform.position);
    }
}
