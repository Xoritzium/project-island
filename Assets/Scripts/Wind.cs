using System;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField]
    private bool UseRandomWind = false;

    [SerializeField]
    private int randomSeed = 2500;

    private float windStrenth;
    /// <summary>
    /// fired whenever the strength of the wind changes.
    /// </summary>
    public static event Action<float> OnStrengthUpdate;

    public static Vector2 Direction { get; private set; }

    void Start()
    {
        if (UseRandomWind)
        {

            UnityEngine.Random.InitState(randomSeed);
            Direction = new(UnityEngine.Random.value, UnityEngine.Random.value);
        }
        else
        {
            Direction = Vector2.right;
        }
        Debug.Log($"Wind Direction is {Direction}");
    }
}
