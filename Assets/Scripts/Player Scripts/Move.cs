using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    Rigidbody2D myBody;
    GameObject player;
    InputProcessor inputProcessor;
    WallJumping wallJumping;
    CheckGrounded checkGrounded;
    PlayerHealth playerHealthScript;

    public float maxSpeed;
    public float jumpVel = 12;
    public float doubleJumpVel = 6;
    public float dashVelX;
    public float dashTimer;
    public float justWallJumpedTimer;
    public Vector2 wallJumpClimb, wallJumpOff, wallLeap;
    public Vector2 knockBack;


    private float velX, velY;
    private float dir;
    private float maxFallSpeed = -13f;
    private float maxFallTimer;

    private float jumpVelX;                     //Place holder for Wall jump Function
    private bool justWallJumped;

    public float knockBackDuration;
    private bool justKnockedBack;
    




    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        inputProcessor = player.GetComponent<InputProcessor>();
        checkGrounded = player.GetComponent<CheckGrounded>();
        myBody = gameObject.GetComponent<Rigidbody2D>();
        playerHealthScript = gameObject.GetComponent<PlayerHealth>();
        wallJumping = GameObject.Find("WallCheck").GetComponent<WallJumping>();

    }

    private void Start()
    {
        maxFallTimer = 0f;
    }

    // Update is called once per frame
    void FixedUpdate() {

        dir = inputProcessor.Dir;

        velX = maxSpeed * dir;
        velY = myBody.velocity.y;

        Jump();
        WallJumping();
        Dash();
        KnockBack();

        if (velY <= maxFallSpeed)
        {
            velY = maxFallSpeed;
            maxFallTimer += Time.deltaTime;
        }
        else
        {
            maxFallTimer = 0;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velX, velY);
    }


    private void KnockBack()
    {
        if (playerHealthScript.KnockBack)
        {
            if (playerHealthScript.EnemyRightSide)
            {
                inputProcessor.IsFacingRight = true;
                inputProcessor.CanMove = false;
                justKnockedBack = true;
                StartCoroutine(JustKnockedBack());

            }

            else
            {
                inputProcessor.IsFacingRight = false;
                inputProcessor.CanMove = false;
                justKnockedBack = true;
                StartCoroutine(JustKnockedBack());
            }

            playerHealthScript.KnockBack = false;
        }

        if (justKnockedBack)
        {
            if (inputProcessor.IsFacingRight)
            {
                velX = -knockBack.x;
                velY = knockBack.y;
            }

            else
            {
                velX = knockBack.x;
                velY = knockBack.y;
            }
            
        }
    }

    private void Dash()
    {
        if (inputProcessor.Dash)
        {
            inputProcessor.CanMove = false;
            if (inputProcessor.IsFacingRight)
            {
                velX = dashVelX;
            }

            else
            {
                velX = -dashVelX;
            }

            velY = 0;
            StartCoroutine(JustDashed());
        }
    }
    
    private void WallJumping()
    {
        if (wallJumping.CollidedWall)
        {
            velY = wallJumping.CollideWallFall;
            
            if (inputProcessor.WallSticking)
            {
                velX = 0;
            }

            if (inputProcessor.WallJumped)
            {
                
                if (inputProcessor.IsFacingRight)
                {
                    if (inputProcessor.WallClimb)
                    {
                        velX = wallJumpClimb.x;
                        velY = wallJumpClimb.y;
                    }

                    else if (inputProcessor.WallJumpOff)
                    {
                        velX = wallJumpOff.x;
                        velY = wallJumpOff.y;
                    }

                    else if (inputProcessor.WallLeap)
                    {
                        velX = wallLeap.x;
                        velY = wallLeap.y;
                    }
                    
                    jumpVelX = velX;

                }

                else
                {

                    if (inputProcessor.WallClimb)
                    {
                        velX = -wallJumpClimb.x;
                        velY = wallJumpClimb.y;
                    }

                    else if (inputProcessor.WallJumpOff)
                    {
                        velX = -wallJumpOff.x;
                        velY = wallJumpOff.y;
                    }

                    else if (inputProcessor.WallLeap)
                    {
                        velX = -wallLeap.x;
                        velY = wallLeap.y;
                    }

                    jumpVelX = velX;

                }

                inputProcessor.WallJumped = false;

                justWallJumped = true;
                StartCoroutine(JustWallJumped());
            }
        }

        //Ensure the player is still moving the direction he wall-jumped from so he doesn't change direction instantly
        //after the wall-jump.
        if (justWallJumped)
        {
           velX = jumpVelX;
        }
    }

    private void Jump()
    {
        //Single Jump
        if (inputProcessor.Jumped)
        {
            velY = jumpVel;
            inputProcessor.Jumped = false;
        }

        //Double Jump
        else if (inputProcessor.DoubleJumped)
        {
            velY = doubleJumpVel;
            inputProcessor.DoubleJumped = false;
            inputProcessor.CanDoubleJump = false;
        }
    }

    IEnumerator JustWallJumped()
    {
        yield return new WaitForSeconds(justWallJumpedTimer);

        justWallJumped = false;
    }

    IEnumerator JustDashed()
    {
        yield return new WaitForSeconds(dashTimer);

        inputProcessor.Dash = false;
        inputProcessor.CanMove = true;
    }

    IEnumerator JustKnockedBack()
    {
        yield return new WaitForSeconds(knockBackDuration);
        justKnockedBack = false;
        inputProcessor.CanMove = true;
    }
    
    public float VelY
    {
        get
        {
            return velY;
        }

        set
        {
            velY = value;
        }
    }

    public float VelX
    {
        get
        {
            return velX;
        }

        set
        {
            velX = value;
        }
    }

    public float MaxFallSpeed
    {
        get
        {
            return maxFallSpeed;
        }

        set
        {
            maxFallSpeed = value;
        }
    }

    public float MaxFallTimer
    {
        get
        {
            return maxFallTimer;
        }

        set
        {
            maxFallTimer = value;
        }
    }
}
