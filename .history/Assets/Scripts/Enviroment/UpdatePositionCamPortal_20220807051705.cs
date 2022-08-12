using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePositionCamPortal : MonoBehaviour
{

    public CinemachineVirtualCamera cam;

    void Update()
    {
        //transform.position = portal.transform.position + new Vector3(0f, 5f, 0f);
        Debug.Log("Posição da camera: " + cam.transform.position);
        Debug.Log("Posição do Portal: "  + transform.position);
    }
}
