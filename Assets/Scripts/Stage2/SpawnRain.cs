using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRain : MonoBehaviour
{
    [SerializeField] GameObject rain;
    [SerializeField] float spawnRate = 1f;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true) 
        {
            Instantiate(rain);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
