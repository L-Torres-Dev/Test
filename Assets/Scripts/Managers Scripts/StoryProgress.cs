using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class that will keep track of Story Progression.
 * Anytime an event takes place in a IStoryTeller, the
 * StoryProgress object will record it to allow the player
 * to further progress through the story.
 */
public class StoryProgress
{

    //Dictionary of conditions to be met to advance story progression
    public Dictionary<string, bool> conditionsDictionary;

    public StoryProgress()
    {
        //Instantiate the Dictionary of conditions
        conditionsDictionary = new Dictionary<string, bool>();

        //Introduction conditions
        conditionsDictionary.Add("Read Histories", false);
    }

}
