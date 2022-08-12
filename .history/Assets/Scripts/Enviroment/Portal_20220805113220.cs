using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Portal : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;

    void Start()
    {
        cinemachineVirtualCamera.transform.position = transform.position;
    }

}