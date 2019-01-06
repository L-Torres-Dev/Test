using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * MonoBehaviour that will affect the camera's target position based on the platform
 * the player is grounded on.
 */ 
public class CameraOnPlatform : MonoBehaviour {

    GameObject player;
    InputProcessor inputProcessor;
    CheckGrounded checkGrounded;
    Move moveScript;

    public float xoffsetRight = 1.5f;
    public float yoffsetRight = 2;
    public float xoffsetLeft = 1.5f;
    public float yoffsetLeft = 2;
    
    
    // Use this for initialization
    void Awake () {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        inputProcessor = player.GetComponent<InputProcessor>();
        checkGrounded = player.GetComponent<CheckGrounded>();
        moveScript = player.GetComponent<Move>();
    }

    public float XoffsetRight
    {
        get
        {
            return xoffsetRight;
        }

        set
        {
            xoffsetRight = value;
        }
    }

    public float YoffsetRight
    {
        get
        {
            return yoffsetRight;
        }

        set
        {
            yoffsetRight = value;
        }
    }

    public float XoffsetLeft
    {
        get
        {
            return xoffsetLeft;
        }

        set
        {
            xoffsetLeft = value;
        }
    }

    public float YoffsetLeft
    {
        get
        {
            return yoffsetLeft;
        }

        set
        {
            yoffsetLeft = value;
        }
    }
}
