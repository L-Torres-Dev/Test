using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float speed;
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * gameObject.GetComponent<InputProcessor>().dir, 0);
	}
}
