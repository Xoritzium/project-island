using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{

    [SerializeField]
    private float minSpawnRadius = 150;
    [SerializeField]
    private float maxSpawnRadius = 300;
    [SerializeField]
    private float minSpawnedIslands = 1;
    [SerializeField]
    private float maxSpawnIslands = 5;

    private List<Island> aliveIslands = new();

    private void Start()
    {
        StartCoroutine(SpawnIslands());
    }

    /// <summary>
    /// Spawn a calculated random amount of islands.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnIslands()
    {
        yield return null;
    }

    private void SinkIslands()
    {

    }

}
