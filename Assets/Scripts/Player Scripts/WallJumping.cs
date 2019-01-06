using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumping : MonoBehaviour {

    private Rigidbody2D myBody;
    GameObject player;
    CheckGrounded checkGrounded;
    InputProcessor inputProcessor;
    Move moveScript;

    private float collideWallFall = -4f;
    private bool collidedWall;
    private bool justExited;

	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        checkGrounded = player.GetComponent<CheckGrounded>();
        inputProcessor = player.GetComponent<InputProcessor>();
        moveScript = player.GetComponent<Move>();
        myBody = player.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Tags.WALL_TAG && !checkGrounded.Hit && myBody.velocity.y <= 0)
        {
            

            if (collision.transform.position.x > transform.position.x && !checkGrounded.Hit && inputProcessor.Dir > 0)
            {
                collidedWall = true;
                inputProcessor.IsFacingRight = false;
                inputProcessor.Dash = false;
                inputProcessor.CanDash = true;
            }

            else if (collision.transform.position.x < transform.position.x && !checkGrounded.Hit && inputProcessor.Dir < 0)
            {
                collidedWall = true;
                inputProcessor.IsFacingRight = true;
                inputProcessor.Dash = false;
                inputProcessor.CanDash = true;
            }
        }

        if (checkGrounded.Hit)
        {
            collidedWall = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Tags.WALL_TAG)
        {
            if (!checkGrounded.Hit)
            {
                //StartCoroutine(JustExited());
                collidedWall = false;
            }
            
            
        }
    }

    IEnumerator JustExited()
    {
        yield return new WaitForSeconds(.15f);

        collidedWall = false;
        justExited = false;
        UnityEngine.Debug.Log("Exited");
    }

    public float CollideWallFall
    {
        get
        {
            return collideWallFall;
        }

        set
        {
            collideWallFall = value;
        }
    }

    public bool CollidedWall
    {
        get
        {
            return collidedWall;
        }

        set
        {
            collidedWall = value;
        }
    }

}
