using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryHallStoryTeller : IStoryTeller
{

    public LibraryHallStoryTeller(StoryProgress storyProgress)
    {

    }

    public bool CheckCollectable(StoryProgress storyProgress, GameObject collectable)
    {
        throw new System.NotImplementedException();
    }

    public Dialogue CheckProgress(StoryProgress storyProgress, NPC npc)
    {
        throw new System.NotImplementedException();
    }

    public void TellStory(StoryProgress storyProgress)
    {
        if (!storyProgress.conditionsDictionary["Read Histories"])
        {
            Utility.FindObject(GameObject.Find("Entrances"), "Entrance 2").SetActive(false);
        }

        else
        {
            Utility.FindObject(GameObject.Find("Entrances"), "Entrance 2").SetActive(true);
            Utility.Log("I've read the histories");
        }
    }
}
