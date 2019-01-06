using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProcessor : MonoBehaviour {

    GameObject player;
    GameObject pausePanel;
    CheckGrounded checkGrounded;
    Move moveScript;
    WallJumping wallJumpingScript;
    
    
    private float dir;
    private bool canControl;

    private bool canMove;
    private bool isFacingRight;

    KeyCode moveRight = KeyCode.RightArrow;
    KeyCode moveLeft = KeyCode.LeftArrow;
    KeyCode jump = KeyCode.Space;
    KeyCode primarySpellKey = KeyCode.F;
    KeyCode secondarySpellKey = KeyCode.D;
    KeyCode dashKey = KeyCode.LeftShift;
    KeyCode pauseKey = KeyCode.Escape;
    KeyCode actionKey = KeyCode.E;

    //jump variables
    private bool jumped;
    private bool justJumped;
    private bool doubleJumped;
    private bool canDoubleJump;

    //wallJump variables
    private bool wallJumped;
    private bool wallSticking;
    private bool wallJumping;
    private bool wallClimb, wallJumpOff, wallLeap;
    public float wallStick;
    public float wallJumpDuration;

    //dash variables
    private bool dash;
    private bool canDash;
    private bool justDashed;

    //World Interaction variables
    private bool touchingNPC;
    private bool touchingCollectable;
    private bool inDialogue;

    //World Interaction objects
    GameObject npcTouch;
    GameObject collectableTouch;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        pausePanel = Utility.FindObject(GameObject.Find("Canvas"), "Pause Panel");
        checkGrounded = player.GetComponent<CheckGrounded>();
        moveScript = player.GetComponent<Move>();
        wallJumpingScript = GameObject.Find("WallCheck").GetComponent<WallJumping>();
    }

    // Use this for initialization
    void Start () {
        isFacingRight = true;
        canMove = true;
        canDash = true;
        canControl = true;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == Tags.NPC_TAG)
        {
            touchingNPC = true;
            npcTouch = collision.transform.parent.gameObject;
        }

        if (collision.tag == Tags.COLLECTABLE_TAG)
        {
            touchingCollectable = true;
            collectableTouch = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Tags.NPC_TAG)
        {
            touchingNPC = false;
            npcTouch = null;
        }

        if (collision.tag == Tags.COLLECTABLE_TAG)
        {
            touchingCollectable = false;
            collectableTouch = null;
        }
    }

    // Update is called once per frame
    void Update () {
        if (canControl)
        {
            Action();
            MoveInput();
            WallJumpingInput();
            JumpInput();
            SpellInput();
            DashInput();
            Flip();
            Menu();
        }
    }

    private void Menu()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (!pausePanel.activeSelf)
            {
                UnityEngine.Debug.Log("Pausing....");
                pausePanel.SetActive(true);
                pausePanel.GetComponent<GameplayMenuScript>().Active(true);
            }
        }   
    }

    private void DashInput()
    {
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            dash = true;
            justDashed = true;
            canDash = false;
            StartCoroutine(Dashed());
        }
    }

    private void SpellInput()
    {
        //Primary Spell
        if (Input.GetKeyDown(primarySpellKey))
        {
            GameObject spell = GameObject.Find("Spells").GetComponent<SpellPooling>().PoolSpell(0);
            spell.transform.position = GameObject.Find("SpellShoot").transform.position;
            spell.SetActive(true);
            Spell spellScript = spell.GetComponent<Spell>();
            
            if (isFacingRight)
            {
                spell.GetComponent<Rigidbody2D>().velocity = spellScript.velocity;
            }

            else
            {
                spell.GetComponent<Rigidbody2D>().velocity = new Vector2(-spellScript.velocity.x, spellScript.velocity.y);
            }
            
        }

        //Secondary Spell
        if (Input.GetKeyDown(secondarySpellKey) && gameObject.GetComponent<EquippedSpells>().SecondarySpell.GetComponent<Spell>().OnCooldown)
        {
            UnityEngine.Debug.Log("Secondary Spell");
            GameObject spell = GameObject.Find("Spells").GetComponent<SpellPooling>().PoolSpell(1);
            spell.transform.position = GameObject.Find("SpellShoot").transform.position;
            spell.SetActive(true);
            Spell spellScript = spell.GetComponent<Spell>();

            if (isFacingRight)
            {
                spell.GetComponent<Rigidbody2D>().velocity = spellScript.velocity;
            }

            else
            {
                spell.GetComponent<Rigidbody2D>().velocity = new Vector2(-spellScript.velocity.x, spellScript.velocity.y);
            }

            gameObject.GetComponent<EquippedSpells>().SecondarySpell.GetComponent<Spell>().SetCooldown();
        }
    }

    private void WallJumpingInput()
    {
        if (wallJumpingScript.CollidedWall)
        {
            if (Input.GetKeyDown(jump))
            {
                wallJumped = true;
                wallJumping = true;
                StartCoroutine(WallJumping());
            }

            if (dir > 0 && !wallSticking && isFacingRight)
            {
                wallSticking = true;
                StartCoroutine(WallStick());
            }

            else if (dir < 0 && !wallSticking && !isFacingRight)
            { 
                wallSticking = true;
                StartCoroutine(WallStick());
            }

            DetermineWallJumpType();
        }
    }

    private void Flip()
    {
        if (!isFacingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

    }

    private void DetermineWallJumpType()
    {
        if (wallJumping)
        {
            if (isFacingRight)
            {
                if (dir < 0)
                {
                    wallClimb = true;
                    wallJumpOff = false;
                    wallLeap = false;
                }
                else if (dir == 0)
                {
                    wallClimb = false;
                    wallJumpOff = true;
                    wallLeap = false;
                }
                else
                {
                    wallClimb = false;
                    wallJumpOff = false;
                    wallLeap = true;
                }
            }

            else if (!isFacingRight)
            {
                if (dir > 0)
                {
                    wallClimb = true;
                    wallJumpOff = false;
                    wallLeap = false;
                }
                else if (dir == 0)
                {
                    wallClimb = false;
                    wallJumpOff = true;
                    wallLeap = false;
                }
                else
                {
                    wallClimb = false;
                    wallJumpOff = false;
                    wallLeap = true;
                }
            }
        }
        else
        {
            wallClimb = false;
            wallJumpOff = false;
            wallLeap = false;
        }
    }

    private void MoveInput()
    {
        if (canMove)
        {
            //Horizontal Movement
            if (Input.GetKey(moveRight))
            {
                dir = 1;
                gameObject.GetComponent<Animator>().SetBool("walking", true);
                if (!wallJumpingScript.CollidedWall)
                {
                    isFacingRight = true;
                }

            }

            else if (Input.GetKey(moveLeft))
            {
                dir = -1;
                gameObject.GetComponent<Animator>().SetBool("walking", true);
                if (!wallJumpingScript.CollidedWall)
                {
                    isFacingRight = false;
                }
            }

            else
            {
                dir = 0;
                gameObject.GetComponent<Animator>().SetBool("walking", false);
            }
        }
        
    }

    private void JumpInput()
    {
        //Jump
        if (checkGrounded.Hit)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumped = true;
                justJumped = true;
                canDoubleJump = true;
                canDash = true;
                justDashed = false;

                StartCoroutine(WaitForJumped());
            }
        }
        /*
        //Double Jump
        if (!checkGrounded.Hit && !doubleJumped && canDoubleJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                doubleJumped = true;
            }
        }*/
    }

    private void Action()
    {
        //Input to take action.
        if (Input.GetKey(actionKey))
        {
            //If the action is taken on an NPC
            if (npcTouch != null)
            {
                UnityEngine.Debug.Log("Action!");
                if (GameObject.Find("Dialogue System").GetComponent<DialogueSystem>().InitiateDialogue(npcTouch))
                {
                    StopControl();
                }

            }

            //If the action is taken on a collectable
            if (collectableTouch != null)
            {
                StoryProgress storyProgress = GameManager.instance.storyManager.storyProgress;

                GameManager.instance.storyManager.storyTeller.CheckCollectable(storyProgress, collectableTouch);
            }
        }
    }

    //Keeps the user from being able to move the player.
    private void StopControl()
    {
        dir = 0;
        canControl = false;
    }

    IEnumerator WallStick()
    {
        yield return new WaitForSeconds(wallStick);
        wallSticking = false;

    }

    IEnumerator WallJumping()
    {
        yield return new WaitForSeconds(wallJumpDuration);
        wallJumping = false;
        canDoubleJump = true;

    }

    IEnumerator Dashed()
    {
        yield return new WaitForSeconds(.5f);
        justDashed = false;
    }

    IEnumerator WaitForJumped()
    {
        yield return new WaitForSeconds(.5f);
        justJumped = false;
        
    }

    public float Dir
    {
        get
        {
            return dir;
        }

        set
        {
            dir = value;
        }
    }

    public bool IsFacingRight
    {
        get
        {
            return isFacingRight;
        }

        set
        {
            isFacingRight = value;
        }
    }

    public bool Jumped
    {
        get
        {
            return jumped;
        }

        set
        {
            jumped = value;
        }
    }

    public bool DoubleJumped
    {
        get
        {
            return doubleJumped;
        }

        set
        {
            doubleJumped = value;
        }
    }

    public bool CanDoubleJump
    {
        get
        {
            return canDoubleJump;
        }

        set
        {
            canDoubleJump = value;
        }
    }

    public bool WallJumped
    {
        get
        {
            return wallJumped;
        }

        set
        {
            wallJumped = value;
        }
    }

    public bool WallSticking
    {
        get
        {
            return wallSticking;
        }

        set
        {
            wallSticking = value;
        }
    }

    public bool WallClimb
    {
        get
        {
            return wallClimb;
        }

        set
        {
            wallClimb = value;
        }
    }

    public bool WallJumpOff
    {
        get
        {
            return wallJumpOff;
        }

        set
        {
            wallJumpOff = value;
        }
    }

    public bool WallLeap
    {
        get
        {
            return wallLeap;
        }

        set
        {
            wallLeap = value;
        }
    }

    public bool Dash
    {
        get
        {
            return dash;
        }

        set
        {
            dash = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return canMove;
        }

        set
        {
            canMove = value;
        }
    }

    public bool CanDash
    {
        get
        {
            return canDash;
        }

        set
        {
            canDash = value;
        }
    }

    public bool JustDashed
    {
        get
        {
            return justDashed;
        }

        set
        {
            justDashed = value;
        }
    }

    public bool CanControl
    {
        get
        {
            return canControl;
        }

        set
        {
            canControl = value;
        }
    }

    public bool JustJumped
    {
        get
        {
            return justJumped;
        }

        set
        {
            justJumped = value;
        }
    }
}
