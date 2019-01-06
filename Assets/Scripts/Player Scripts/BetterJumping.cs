using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumping : MonoBehaviour {

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 1f;
    public float riseMultiplier;

    public float maxFallVel = -5f;

    Rigidbody2D myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (myBody.velocity.y < 0)
        {
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier ) * Time.deltaTime;
        }

        else if (myBody.velocity.y > 0 && Input.GetKey(KeyCode.Space))
        {
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (riseMultiplier - 1) * Time.deltaTime;
        }

        else if (myBody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        
    }
}
