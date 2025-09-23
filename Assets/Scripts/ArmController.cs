using UnityEngine;
using System.Collections;

public class ArmController : MonoBehaviour
{
    public Transform arm;            // Drag your Arm transform here
    public Rigidbody2D armRb;        // Drag Arm's Rigidbody2D here
    public Rigidbody2D bodyRb;       // Drag Body's Rigidbody2D here

    public float torqueStrength = 200f;   // Tune this!

    void FixedUpdate()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorld - bodyRb.transform.position;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = armRb.rotation;

        float angleDiff = Mathf.DeltaAngle(currentAngle, targetAngle);

        float torque = angleDiff * torqueStrength;

        armRb.AddTorque(torque);
    }
}

