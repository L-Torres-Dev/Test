using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * GameManager Singleton that keeps track of the current state of the game.
 * The static instance holds a storyManager object that helps keep track of 
 * the story. (Keeps the state of the game more organized and allows for modulararity)
 */
public class GameManager : MonoBehaviour {

    public static GameManager instance = null;                  //The Static GameManager instance being used as the Singleton

    public StoryManager storyManager;                           //StoryManager that keeps track of the story progress
    public string spawnName;                                    //Name of the Spawn point that the player will spawn in. (Dependant on where the player is coming from)

	// Use this for initialization
	void Awake () {
        
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;

            //Load this scene's set of NPCs
            XMLDialogueScriptReader.LoadNPCs();

            //Instantiate a new StoryManager Object and set it to the static instance's storyManager member
            instance.storyManager = new StoryManager();

            //Delegate the TellSceneStory to the SceneLoaded event
            Delegate();
        }
        
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    

    /*
     * Delegates the TellSceneStory() function to the sceneLoaded event so that whenever a scene is loaded the IStoryTeller
     * will call its TellStory() function implemented from the IStoryTeller Interface
     */
    private void Delegate()
    {
        SceneManager.sceneLoaded += TellSceneStory;

    }

    /*
     * Calls an IStoryTeller's TellStory() function (The current IStoryTeller in use)
     */
    private void TellSceneStory(Scene scene, LoadSceneMode mode)
    {
        GameObject.Find("SceneStoryManager").GetComponent<SceneStoryManager>().SetPath();
        XMLDialogueScriptReader.LoadNPCs();

        storyManager.npcList = XMLDialogueScriptReader.npcList;
        storyManager.npcs = XMLDialogueScriptReader.npcs;

        storyManager.DetermineStoryTeller();
        SetPlayerSpawn();
    }

    private void SetPlayerSpawn()
    {
        //Get a reference to the Player Object
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //Create spawn reference variable to reference which spawn point the player will spawn in.
        GameObject spawn;        

        //If there is no current spawn point (This should technically only happen in the beginning of the game unless special cases arise)
        if (instance.spawnName == "")
        {
            //Spawn in the default spawn point (The default spawn point should always be at index 1 - NOTE: index 0 is the parent transform object)
            spawn = Utility.FindObject(GameObject.Find("Spawns"), 1);
        }

        //If there is a current spawn point (This should happen most of the time throughout the game.)
        else
        {
            //Set the spawn Game Object reference to the spawn GameObject being referenced the instance.spawnName
            spawn = Utility.FindObject(GameObject.Find("Spawns"), instance.spawnName);
        }

        //Set the player's position to the spawn point.
        player.transform.position = spawn.transform.position;

        //Set the main Camera's position to the player's position
        GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA).transform.position = player.transform.position;
    }

    //Getter for the storyManager Object (May not be necessary)
    public StoryManager getStoryManager()
    {
        return storyManager;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
