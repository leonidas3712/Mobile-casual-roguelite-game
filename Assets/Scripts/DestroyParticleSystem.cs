using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{
    [SerializeField]
    ParticleSystem ps;
    void Start()
    {
        Destroy(gameObject, ps.main.startLifetime.constantMax);
    }
}
