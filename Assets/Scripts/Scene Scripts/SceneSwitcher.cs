using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Used to transition between scenes when the player leaves an area (scene is synonymous to area)
 */
public class SceneSwitcher : MonoBehaviour
{

    public string sceneName;                    //The name of the scene that this SceneSwitcher transitions to
    public string spawnName;                    //The name of the spawn point of the scene's spawn point that the player will start in upon successful scene transition

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the player ever collides to the SceneSwitcher
        if (collision.tag == "Player")
        {
            
            //Set the GameManager instance's spawnName to this SceneSwitcher's spawnName.
            GameManager.instance.spawnName = spawnName;

            //Load the Scene with this SceneSwitcher's sceneName.
            SceneManager.LoadScene(sceneName);
        }
    }
}