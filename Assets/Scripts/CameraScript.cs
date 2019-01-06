using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    GameObject player;
    InputProcessor inputProcessor;
    CheckGrounded checkGrounded;
    Move moveScript;

    private float  panSpeed = 2;
    private float camSpeed;
    private float lastXoffset, lastYoffset;
    private float XoffsetRight = 5, YoffsetRight = 2;           //Camera offset position relative to the player when facing Right
    private float XoffsetLeft = 5, YoffsetLeft = 2;             //Camera offset position relative to the player when facing Left

    Vector3 lastPos;

    // Use this for initialization
    void Awake () {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        inputProcessor = player.GetComponent<InputProcessor>();
        checkGrounded = player.GetComponent<CheckGrounded>();
        moveScript = player.GetComponent<Move>();

        lastPos = gameObject.transform.position;
        panSpeed = 2f;
    }
	
	// Update is called once per frame
	void Update () {

        if (checkGrounded.Hit)
        {
            //Determine the camera Offsets based on which platform the player is currently standing on
            if (checkGrounded.Hit1)
            {
                XoffsetRight = checkGrounded.Hit1.collider.GetComponent<CameraOnPlatform>().XoffsetRight;
                YoffsetRight = checkGrounded.Hit1.collider.GetComponent<CameraOnPlatform>().YoffsetRight;

                XoffsetLeft = checkGrounded.Hit1.collider.GetComponent<CameraOnPlatform>().XoffsetLeft;
                YoffsetLeft = checkGrounded.Hit1.collider.GetComponent<CameraOnPlatform>().YoffsetLeft;
            }

            else if (checkGrounded.Hit2)
            {
                XoffsetRight = checkGrounded.Hit2.collider.GetComponent<CameraOnPlatform>().XoffsetRight;
                YoffsetRight = checkGrounded.Hit2.collider.GetComponent<CameraOnPlatform>().YoffsetRight;

                XoffsetLeft = checkGrounded.Hit2.collider.GetComponent<CameraOnPlatform>().XoffsetLeft;
                YoffsetLeft = checkGrounded.Hit2.collider.GetComponent<CameraOnPlatform>().YoffsetLeft;
            }
            
        }
        
        // If player is Grounded
        if (checkGrounded.Hit)
        {
            Vector3 startPos;
            Vector3 destination;

            if (inputProcessor.IsFacingRight)
            {
                startPos = gameObject.transform.position;
                destination = new Vector3(player.transform.position.x + XoffsetRight, player.transform.position.y + YoffsetRight, -10);
                panSpeed = AdjustPanSpeed(startPos, destination);

                lastXoffset = XoffsetRight;
                lastYoffset = YoffsetRight;
                lastPos = startPos;
            }

            else
            {
                startPos = gameObject.transform.position;
                destination = new Vector3(player.transform.position.x - XoffsetLeft, player.transform.position.y + YoffsetLeft, -10);
                panSpeed = AdjustPanSpeed(startPos, destination);

                lastXoffset = XoffsetLeft;
                lastYoffset = YoffsetLeft;
                lastPos = startPos;
            }

            gameObject.transform.position = Vector3.Lerp(startPos, destination, panSpeed * Time.deltaTime);
        }

        //If player is not grounded (In mid-air)
        else
        {
            Vector3 startPos;
            Vector3 destination;
            float destinationY;

            if (inputProcessor.IsFacingRight)
            {
                startPos = gameObject.transform.position;
                if (player.GetComponent<Rigidbody2D>().velocity.y <= moveScript.MaxFallSpeed)
                {
                    destinationY = player.transform.position.y - 2f;
                }
                else
                {
                    destinationY = player.transform.position.y + lastYoffset;
                }
                destination = new Vector3(player.transform.position.x + lastXoffset, destinationY, -10);
                panSpeed = AdjustPanSpeed(startPos, destination);
                lastPos = startPos;
            }

            else
            {
                startPos = gameObject.transform.position;
                if (player.GetComponent<Rigidbody2D>().velocity.y <= moveScript.MaxFallSpeed)
                {
                    destinationY = player.transform.position.y - 2f;
                }
                else
                {
                    destinationY = player.transform.position.y + lastYoffset;
                }
                destination = new Vector3(player.transform.position.x - lastXoffset, destinationY, -10);
                panSpeed = AdjustPanSpeed(startPos, destination);
                lastPos = startPos;
            }

            gameObject.transform.position = Vector3.Lerp(startPos, destination, panSpeed * Time.deltaTime);
        }
    }

    //Function to adjust the speed of the camera based on the distance it needs to travel, player's current speed, etc.
    private float AdjustPanSpeed(Vector3 start, Vector3 destination)
    {
        float distance = Vector3.Distance(start, destination);
        float playerSpeed = Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x);

        camSpeed = Vector3.Distance(lastPos, start) / Time.deltaTime;

        if (player.GetComponent<Rigidbody2D>().velocity.y <= moveScript.MaxFallSpeed && moveScript.MaxFallTimer >= .5f)
        {
            panSpeed = Mathf.Lerp(panSpeed, 6.5f, .9f * Time.deltaTime);
        }

        else if (distance >= 3 && distance <= 5 && (playerSpeed - camSpeed > 1))
        {
            panSpeed = 1.5f;
        }

        else if (inputProcessor.Dir != 0 && distance < 1)
        {
            panSpeed = 1f;
        }

        else if(inputProcessor.Dir != 0)
        {
            panSpeed = 3;
        }

        else
        {
            panSpeed = 2;
        }
        return panSpeed;
    }

    public float PanSpeed
    {
        get
        {
            return panSpeed;
        }

        set
        {
            panSpeed = value;
        }
    }

    public float CamSpeed
    {
        get
        {
            return camSpeed;
        }
        set
        {
            camSpeed = value;
        }
    }
}
