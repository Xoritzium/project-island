using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IslandGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject islandPrefab;

    #region static variables
    [Header("Proximity Radii")]
    [SerializeField]
    public static float ProximityRadius;

    #endregion


    #region fields
    [Header("Spawn Settings")]
    [SerializeField]
    private float minSpawnRadius = 150;
    [SerializeField]
    private float maxSpawnRadius = 300;
    [SerializeField]
    private float minSpawnedIslands = 1;
    [SerializeField]
    private float maxSpawnIslands = 5;

    public float Tickrate
    {
        get
        {
            return tickrate;
        }
    }

    #endregion
    #region variables
    [Header("Time")]
    [SerializeField, Tooltip("tickrate to reevaluate living island")]
    private float tickrate;
    [SerializeField]
    private float minAlive;
    [SerializeField]
    private float maxAlive;

    [SerializeField]
    private bool spawn = true;

    private Ship ship;
    private int RandomDirectionSeed = 12;

    private int counter = 0;

    private int AliveIslands = 0;

    #endregion
    private void Start()
    {
        ship = GameObject.FindFirstObjectByType<Ship>();
        StartCoroutine(SpawnIslands());
    }

    /// <summary>
    /// Spawn a calculated random amount of islands.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnIslands()
    {
        while (spawn)
        {
            if (this.AliveIslands == maxSpawnIslands) continue;
            SpawnIsland();
            yield return new WaitForSeconds(tickrate);
        }
    }

    private void SpawnIsland()
    {
        //Random.InitState(this.RandomDirectionSeed);
        Vector3 direction = new(Random.value, 0, Random.value);
        Island island = GameObject.Instantiate(islandPrefab).GetComponent<Island>();
        island.Init(this.ship, Random.Range(minAlive, maxAlive));
        island.name = "Island_" + ++counter;
        island.transform.position = direction.normalized * Random.Range(minSpawnRadius, maxSpawnRadius);
        island.OnSink += this.SinkIsland;
        ++AliveIslands;
    }


    private void SinkIsland(Island island)
    {
        Debug.Log($"Destroying {island.name}");
        Destroy(island.gameObject);
        --AliveIslands;
    }
}
