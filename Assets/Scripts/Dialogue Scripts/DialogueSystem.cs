using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * MonoBehavior class that Handles Dialogue with NPCs.
 */
public class DialogueSystem : MonoBehaviour
{

    //Reference to the StoryManager Object.
    StoryManager storyManager;

    public Text text;                               //Dialogue Text box that displays when User is in a dialogue
    InputProcessor inputProcessor;                  //Reference for the player's input processor script that will be used to grant control after the end of a Dialogue.

    private bool inDialogue;                        //Boolean that keeps track of when the user is in a Dialogue.
    private bool canDialogue;                       //Boolean that keeps track of when the user can enter a Dialogue.
    private int currentSentenceIndex = 0;           //Int that keeps track of which sentence in a Dialogue the user is currently in.

    private KeyCode action = KeyCode.Space;         //KeyCode that is used to progress through a Dialogue (It's the same as the Player Script's action key)

    public NPC npcInDialogue;                       //Current NPC the user is in Dialogue with.
    private Dialogue currentDialogue;               //Current Dialogue the user is in with the NPC. (NPC's can have multiple dialogues used for different areas of the story)


    // Use this for initialization
    void Start()
    {
        inDialogue = false;
        canDialogue = true;
        inputProcessor = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<InputProcessor>();
        storyManager = GameObject.FindGameObjectWithTag(Tags.GAMEMANAGER_TAG).GetComponent<GameManager>().storyManager;

    }

    // Update is called once per frame
    void Update()
    {
        Dialogue();
    }

    //Initiates a Dialogue as long as the Dialogue System allows for it.
    public bool InitiateDialogue(GameObject npc)
    {
        if (canDialogue)
        {
            
            //Set the current NPC for this dialogue to the npc that matches the name of the npc passed to this function for the storyManager's npcs dictionary.
            npcInDialogue = storyManager.npcs[npc.name];

            //Check progress of story and choose the dialogue on that basis
            currentDialogue = storyManager.storyTeller.CheckProgress(storyManager.storyProgress, npcInDialogue);

            //Start the Dialogue
            text.gameObject.SetActive(true);
            text.text = currentDialogue.sentences[currentSentenceIndex];

            //Make sure the Dialogue System knows the user is in a Dialogue.
            inDialogue = true;


            return true;
        }

        return false;

    }

    //Handles the continuation of a Dialogue that has been initiated.
    private void Dialogue()
    {

        //Checks to make sure the user is in a Dialogue.
        if (inDialogue)
        {
            //If the user hits the action key while in dialogue.
            if (Input.GetKeyDown(action))
            {

                //Check to make sure the Player isn't at the end of a dialogue.
                if (currentSentenceIndex < currentDialogue.sentences.Count)
                {
                    text.text = currentDialogue.sentences[currentSentenceIndex];
                    currentSentenceIndex++;
                }

                //End the dialogue.
                else
                {
                    canDialogue = false;
                    EndDialogue();
                    StartCoroutine(JustTalked());
                }

            }
        }
    }

    //Ends the dialogue.
    private void EndDialogue()
    {
        npcInDialogue = null;
        currentDialogue = null;
        currentSentenceIndex = 0;
        text.text = "";
        text.gameObject.SetActive(false);
        inDialogue = false;
        inputProcessor.CanControl = true;
    }

    /*
     * At the end of a dialogue, dialogues are disabled for a short duration to keep player from
     * immediately re-entering a dialogue after ending it.
     */
    IEnumerator JustTalked()
    {
        yield return new WaitForSeconds(.5f);
        canDialogue = true;
    }

    public bool InDialogue
    {
        get
        {
            return inDialogue;
        }

        set
        {
            inDialogue = value;
        }
    }
}
