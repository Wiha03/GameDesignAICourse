using System.Collections;
using UnityEngine;

public class gravity : MonoBehaviour
{
    public Rigidbody2D rb;
    public float baseGravity = 2f;
    public float maxfallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
    }

    public void Gravity()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxfallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
}
