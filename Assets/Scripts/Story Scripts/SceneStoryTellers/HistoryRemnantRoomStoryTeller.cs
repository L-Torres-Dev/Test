using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryRemnantRoomStoryTeller : IStoryTeller
{
    
    public HistoryRemnantRoomStoryTeller(StoryProgress storyProgress)
    {
        
    }
    

    public void TellStory(StoryProgress storyProgress)
    {
        if (!storyProgress.conditionsDictionary["Read Histories"])
        {
            Utility.FindObject(GameObject.Find("Entrances"), "Entrance").SetActive(false);
        }

        else
        {
            Utility.FindObject(GameObject.Find("Entrances"), "Entrance").SetActive(true);
            Utility.Log("I've read the histories");
        }
    }

    public bool CheckCollectable(StoryProgress storyProgress, GameObject collectable)
    {
        return true;
    }

    public Dialogue CheckProgress(StoryProgress storyProgress, NPC npc)
    {
        //Create a reference to a dialogue
        Dialogue dialogue = new Dialogue();

        //Check the name of the NPC since the dialogue chosen will depend on which NPC was chosen. This will also affect
        //which part of the storyProgress will be queried
        switch (npc.name)
        {
            case "Book And Table":
                dialogue = npc.dialogues[0];
                storyProgress.conditionsDictionary["Read Histories"] = true;
                Utility.FindObject(GameObject.Find("Entrances"), "Entrance").SetActive(true);
                break;
            default:
                dialogue = npc.dialogues[0];
                break;
        }
        return dialogue;
    }
    
}
