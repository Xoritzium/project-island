using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    [SerializeField, Range(0, 10)]
    private float speed = 1;
    [SerializeField, Range(5, 45)]
    private float turnAmount = 5;

    [Header("Speed calculation")]

    [SerializeField, Range(1, 100)]
    private float angleInfluenceScale = 1;
    [SerializeField]
    private float minSpeed = 0.1f;
    [SerializeField]
    private float maxSpeed = 10f;

    Vector3 moveDirection = Vector3.forward;

    private ShipInput input;

    void Start()
    {
        input = new();
        input.Keyboard.Enable();

        Wind.OnStrengthUpdate += this.UpdateSpeed;
    }

    void Update()
    {
        if (input.Keyboard.MoveLeft.IsPressed())
        {
            Rotate(true);
        }
        else if (input.Keyboard.MoveRight.IsPressed())
        {
            Rotate(false);
        }
        ApplyMovement();
    }

    private void UpdateSpeed(float newSpeed)
    {
        moveDirection *= newSpeed;
    }
    private void Rotate(bool left)
    {
        this.transform.Rotate(Vector3.up, left ? -turnAmount * Time.deltaTime : turnAmount * Time.deltaTime);
    }

    /// <summary>
    /// Apply movement onto the ship. It calculates the amount to be traveled based on
    /// <see cref="this.speed"/> and the angle to the <see cref="Wind.Direction"/>.
    /// </summary>
    private void ApplyMovement()
    {
        float angle = Vector3.Angle(Wind.Direction, this.transform.forward);

        Vector3 move = moveDirection * speed * MapAngleToSpeed(angle, minSpeed, maxSpeed) * Time.deltaTime;
        this.transform.Translate(move);
    }
    /// <summary>
    ///  Maps from one set of values to another.
    /// TODO: Consider moving into a Util class
    /// </summary>
    private float MapAngleToSpeed(float value, float outMin, float outMax, float inMin = 0, float inMax = 180)
    {
        float remapped = (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
        return remapped * angleInfluenceScale;
    }

}
