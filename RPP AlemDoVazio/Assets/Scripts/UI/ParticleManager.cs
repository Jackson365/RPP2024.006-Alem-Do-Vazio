using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject prefabSmoke;


    private void OnEnable()
    {
        ParticleObserver.ParticleSpawnEvent += SpawnarParticle;
    }

    private void OnDisable()
    {
        ParticleObserver.ParticleSpawnEvent -= SpawnarParticle;
    }

    public void SpawnarParticle(Vector3 pos)
    {
        Instantiate(prefabSmoke, pos, quaternion.identity);
    }
}
