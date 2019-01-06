using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunelightVillageStoryTeller : IStoryTeller
{
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
        switch(storyProgress.conditionsDictionary["Read Histories"])
        {
            case true:
                GameObject.FindGameObjectWithTag("Player").GetComponent<InputProcessor>().CanControl = false;
                break;
        }
    }

    
}
