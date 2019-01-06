using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Interface for a C# Object. Any class that implements this
 * Interface will be unique to each Unity scene and will handle 
 * events based on the progress made in the StoryProgress Object.
 */
public interface IStoryTeller
{

    /*This function will handle what happens in a scene when it is loaded based on the current
     * progress made in the StoryProgress Object.
     */
    void TellStory(StoryProgress storyProgress);

    /*
     * Checks the current progress of the story in the storyProgress object. Then returns a dialogue
     * according to the progress made so far as well as which NPC was interacted with. This function 
     * is called by the Dialogue System.
     */
    Dialogue CheckProgress(StoryProgress storyProgress, NPC npc);

    /*
     * Checks the current progress of the story in the storyProgress object. Then returns a dialogue
     * according to the progress made so far as well as which Collectable/item was interacted with. 
     * This function is called by the Player Script.
     */
    bool CheckCollectable(StoryProgress storyProgress, GameObject collectable);
}
