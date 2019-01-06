using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStoryManager : MonoBehaviour {

    //Filepath for the XML document containing NPC dialogues
    public string npcDialoguesFilePath;

    public void SetPath()
    {
        XMLDialogueScriptReader.path = npcDialoguesFilePath;
    }
}
