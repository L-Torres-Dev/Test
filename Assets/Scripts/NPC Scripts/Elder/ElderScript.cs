using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderScript : MonoBehaviour {

    GameObject player;
    InputProcessor inputProcessor;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        inputProcessor = player.GetComponent<InputProcessor>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
