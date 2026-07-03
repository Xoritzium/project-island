using System;
using UnityEngine;

public class Ship : MonoBehaviour
{

    public event Action<Vector3> PublishTransform;

    public void Awake()
    {
    }

    public void Update()
    {
        PublishTransform.Invoke(this.transform.position);
    }

}