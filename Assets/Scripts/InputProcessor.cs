using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProcessor : MonoBehaviour {

    [HideInInspector]
    public float dir;

    private bool isFacingRight;

	// Use this for initialization
	void Start () {
        isFacingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir = 1;
            gameObject.GetComponent<Animator>().SetBool("isMoving", true);
            isFacingRight = true;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir = -1;
            gameObject.GetComponent<Animator>().SetBool("isMoving", true);
            isFacingRight = false;
        }

        else
        {
            dir = 0;
            gameObject.GetComponent<Animator>().SetBool("isMoving", false);
        }

        Flip();
    }

    protected void Flip()
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
}
