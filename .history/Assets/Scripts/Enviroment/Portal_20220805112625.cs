using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;

    void Start()
    {
        cinemachineVirtualCamera.transform.position = transform.position;
    }

}
