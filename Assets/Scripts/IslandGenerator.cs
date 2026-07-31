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

    [Header("Randomness")]
    [SerializeField, Range(0, 1), Tooltip("The closer to one, the higher the chance, the next spawned island has positive values")]
    private float SignProbability;
    [SerializeField]
    private int RandomDirectionSeed = 12;


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


    private int counter = 0;

    private int aliveIslands = 0;

    #endregion
    private void Start()
    {
        aliveIslands = 0;
        ship = GameObject.FindFirstObjectByType<Ship>();
        //s  StartCoroutine(SpawnIslands());
    }

    /// <summary>
    /// Spawn a calculated random amount of islands.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnIslands()
    {
        while (spawn)
        {
            if (this.aliveIslands == maxSpawnIslands) continue;
            SpawnIsland();
            yield return new WaitForSeconds(tickrate);
        }
    }

    public void SpawnIsland()
    {
        //Random.InitState(this.RandomDirectionSeed); 
        Vector3 direction = new(Sign(SignProbability) * Random.value, 0, Sign(SignProbability) * Random.value);
        Island island = GameObject.Instantiate(islandPrefab).GetComponent<Island>();
        island.Init(this.ship, Random.Range(minAlive, maxAlive));
        island.name = "Island_" + ++counter;
        island.transform.position = direction.normalized * Random.Range(minSpawnRadius, maxSpawnRadius);
        island.OnSink += this.SinkIsland;
        island.transform.parent = this.gameObject.transform;
        ++aliveIslands;
    }


    public void DestroyIslands()
    {
        while (this.transform.childCount > 0)
        {
            if (Application.isEditor)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
            if (Application.isPlaying)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
        aliveIslands = 0;
    }


    private void SinkIsland(Island island)
    {
        Debug.Log($"Destroying {island.name}");
        Destroy(island.gameObject);
        --aliveIslands;
    }

    /// <summary>
    /// Returns 1 or -1, based on the probability
    /// </summary>
    private int Sign(float probability)
    {
        if (Random.value < Mathf.Clamp(probability, 0, 1))
        {
            return 1;
        }
        return -1;
    }
}
