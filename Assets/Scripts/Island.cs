
using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Island : MonoBehaviour
{
    public event Action<Island> OnSink;

    public bool Sinkable { get; set; }

    public float BirthTime { get; set; } // sek

    public float Death { get; set; } //sek

    [SerializeField]
    private float LivingTime; //debug purpose only, remove at some point

    private bool inProximity;

    public void Awake()
    {
        Sinkable = true;
        BirthTime = Time.time;
    }
    public void Init(Ship ship, float death)
    {
      //  ship.PublishTransform += this.Proximity;
        this.Death = death;
        inProximity = true;

    }
/*
    public void Update()
    {
        if (Time.time - BirthTime > Death)
        {
            ScheduleToSink();
        }
        if (!inProximity)
        {
            OnSink?.Invoke(this);
        }

        LivingTime = Time.time - BirthTime; //mainly debug
    }

    private void Proximity(Vector3 playerPos)
    {
        if (Vector3.Distance(playerPos, this.transform.position) > IslandGenerator.ProximityRadius)
        {
            inProximity = false;
        }
        inProximity = true;
    }
    private void ScheduleToSink()
    {

    }
    */
}