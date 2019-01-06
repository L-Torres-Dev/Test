using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * StoryManager Class used to create a single Object that handles
 * what IStoryTeller to use to advance the story. This Class also
 * Instantiates the StoryProgress Object.
 */
public class StoryManager
{

    public IStoryTeller storyTeller;                                        //Reference to the current Object that implements IStoryTeller (This is unique to each Unity Scene)
    public StoryProgress storyProgress;                                     //Reference to the Object that keeps track of the players progress in the story.

    public Dictionary<string, NPC> npcs;                          //Holds the NPC's for the current scene in a dictionary
    public List<NPC> npcList = new List<NPC>();         //Holds the NPC's for the current scene in a list (in case it is necessary)



    public StoryManager()
    {
        npcs = new Dictionary<string, NPC>();
        npcList = new List<NPC>();

        
        npcs = XMLDialogueScriptReader.npcs;                                //Reference the npcs from the XMLReader's NPC's dictionary
        npcList = XMLDialogueScriptReader.npcList;                          //Reference the npcs from the XMLReader's NPC's list                       
        storyProgress = new StoryProgress();                                //Instantiate a StoryProgress.
        
    }

    /*
     * Determines which IStoryTeller to use depending on the current scene.
     */
    public void DetermineStoryTeller()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "00_history_remnant_room":
                storyTeller = new HistoryRemnantRoomStoryTeller(storyProgress);
                storyTeller.TellStory(storyProgress);
                break;
            case "04_library_hall":
                storyTeller = new LibraryHallStoryTeller(storyProgress);
                storyTeller.TellStory(storyProgress);
                break;
            default:
                break;
        }
    }

}