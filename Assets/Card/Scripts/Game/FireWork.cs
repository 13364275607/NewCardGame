using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWork : MonoBehaviour
{
    void Start()
    {
        ParticleSystem particle = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particle.main;
        mainModule.loop = false;
        mainModule.stopAction = ParticleSystemStopAction.Callback;
    }
}
