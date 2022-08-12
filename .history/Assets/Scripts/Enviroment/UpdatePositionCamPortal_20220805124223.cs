using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePositionCamPortal : MonoBehaviour
{

    public GameObject portal;

    void Update()
    {
        transform.position = portal.transform.position;
    }
}
