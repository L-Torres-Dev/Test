using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour {

    private Transform groundCheck1, groundCheck2;
    private Rigidbody2D myBody;
    GameObject player;
    InputProcessor inputProcessor;
    WallJumping wallJumping;
    Move moveScript;

    private RaycastHit2D hit1, hit2;
    private bool hit;
    private bool wasHit;
    
    // Use this for initialization
    void Awake () {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        inputProcessor = player.GetComponent<InputProcessor>();
        moveScript = player.GetComponent<Move>();
        myBody = gameObject.GetComponent<Rigidbody2D>();
        groundCheck1 = GameObject.Find("GroundCheck").GetComponent<Transform>();
        groundCheck2 = GameObject.Find("GroundCheck2").GetComponent<Transform>();
        wallJumping = GameObject.Find("WallCheck").GetComponent<WallJumping>();
    }
	
	// Update is called once per frame
	void Update () {
        Grounded();
	}

    private void Grounded()
    {
        //The starting position of the raycast which will be at the player's GroundCheck Object (At the player's feet)
        Vector2 startingPosition = groundCheck1.transform.position;
        Vector2 startingPosition2 = groundCheck2.transform.position;

        //Casts the ray downward, the ray is a miniscule length of .1 units so that it only detects colliders close to
        //the player's feet
        hit1 = Physics2D.Raycast(startingPosition, new Vector2(0, -1), .1f, ~(1 << 9));
        hit2 = Physics2D.Raycast(startingPosition2, new Vector2(0, -1), .1f, ~(1 << 9));

        wasHit = hit;


        //Set hit variable in a way that gives the player a bit of a delay of forgiveness if he simply falls off a ledge
        //This will give him a chance to jump shortly after falling off a ledge if he hasn't already jumped.
        if((hit1 || hit2) && (Mathf.Abs(myBody.velocity.y) <= .5f))
        {
            hit = true;
        }
        else
        {
            if (wasHit && !inputProcessor.JustJumped)
            {
                StartCoroutine(HitDelay());
            }

            else
            {
                hit = false;
            }

        }

        //if the player character is grounded and hasn't dashed then allow for dashing (This is so that the abitlity to dash resets upon hitting the ground)
        if (!inputProcessor.CanDash && hit && !inputProcessor.JustDashed)
        {
            inputProcessor.CanDash = true;
        }

        //Reset the ability to double jump upon hitting the ground.
        if (hit)
        {
            inputProcessor.CanDoubleJump = true;
        }
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(.05f);

        hit = false;
    }

    public RaycastHit2D Hit1
    {
        get
        {
            return hit1;
        }

        set
        {
            hit1 = value;
        }
    }

    public RaycastHit2D Hit2
    {
        get
        {
            return hit2;
        }

        set
        {
            hit2 = value;
        }
    }

    public bool Hit
    {
        get
        {
            return hit;
        }

        set
        {
            hit = value;
        }
    }

    public bool WasHit
    {
        get
        {
            return wasHit;
        }

        set
        {
            wasHit = value;
        }
    }
}
