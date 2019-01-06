using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug : MonoBehaviour {

    public Text inputProcessor_Debug;
    public Text grounded_Debug;
    public Text move_Debug;
    public Text camera_Debug;
    public Text wallJumping_Debug;

    GameObject player;
    GameObject mainCamera;
    InputProcessor inputProcessor;
    CheckGrounded checkGrounded;
    Move moveScript;
    CameraScript cameraScript;
    WallJumping wallJumping;

    public bool disableDebug;

    private void Awake()
    {
        
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        inputProcessor = player.GetComponent<InputProcessor>();
        checkGrounded = player.GetComponent<CheckGrounded>();
        moveScript = player.GetComponent<Move>();
        mainCamera = GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA);
        cameraScript = mainCamera.GetComponent<CameraScript>();
        wallJumping = GameObject.Find("WallCheck").GetComponent<WallJumping>();
    }

    // Update is called once per frame
    void Update () {
        inputProcessor_Debug.text =  "Direction: " + inputProcessor.Dir + "\njumped: " + inputProcessor.Jumped + 
            "\ndoubleJumped: " + inputProcessor.DoubleJumped + "\nIsFacingRight: " + inputProcessor.IsFacingRight +
            "\nWallSticking: " + inputProcessor.WallSticking;
        grounded_Debug.text = "wasHit: " + checkGrounded.WasHit + "isHit: " + checkGrounded.Hit + " | isHit1: " + (bool) checkGrounded.Hit1 +
            " | isHit2: " + (bool) checkGrounded.Hit2;

        move_Debug.text = "VelX: " + player.GetComponent<Rigidbody2D>().velocity.x + " | VelY: " + moveScript.VelY;

        camera_Debug.text = "PanSpeed: " + cameraScript.PanSpeed + " | Camera Vel: " + cameraScript.CamSpeed;

        wallJumping_Debug.text = "CollidedWall: " + wallJumping.CollidedWall;

        if (disableDebug)
        {
            inputProcessor_Debug.gameObject.SetActive(false);
            grounded_Debug.gameObject.SetActive(false);
            move_Debug.gameObject.SetActive(false);
            camera_Debug.gameObject.SetActive(false);
            wallJumping_Debug.gameObject.SetActive(false);
        }

        else
        {
            inputProcessor_Debug.gameObject.SetActive(true);
            grounded_Debug.gameObject.SetActive(true);
            move_Debug.gameObject.SetActive(true);
            camera_Debug.gameObject.SetActive(true);
            wallJumping_Debug.gameObject.SetActive(true);
        }
	}
}
