using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    CinemachineImpulseSource impulseSorce;

    void Start()
    {
        impulseSorce = gameObject.GetComponent<CinemachineImpulseSource>();
    }

    public void Shake()
    {
        impulseSorce.GenerateImpulse();
    }
}
