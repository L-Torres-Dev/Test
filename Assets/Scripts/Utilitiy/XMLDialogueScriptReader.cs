using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

/*
 * Class that will read an .xml file containing data about npc's and their dialogues
 * 
 * NOTE: the static "npcs" and "npcList" members MUST be accessed using the class name
 * for example: "XMLDialogueScriptReader.npcs" must be used to reference the "npcs" static member
 */
public static class XMLDialogueScriptReader{

    public static string path;
    public static Dictionary<string, NPC> npcs;                                 //Will contain a dictionary of NPCs to be referenced by name.
    public static List<NPC> npcList = new List<NPC>();                          //Will contain a list of NPCs to be referenced by index (Generally used for debug purposes)


    public static void LoadNPCs()
    {
        
        //Dictionary of the npcs to access them by name
        npcs = new Dictionary<string, NPC>();

        //List of the npcs to access them by index
        npcList = new List<NPC>();

        path = GameObject.Find("SceneStoryManager").GetComponent<SceneStoryManager>().npcDialoguesFilePath;

        if (path != "")
        {
            InstantiateNPCsFromXmlDocument(path);
        }
    }


    /*
     *Reads an XML document containing all the NPCs and their dialogues and creates instances
     * of all the NPCs with their data from the data contained in the XML document.
     */
    private static void InstantiateNPCsFromXmlDocument(string path)
    {
        TextAsset xmlfile = Resources.Load<TextAsset>(path);
        var doc = XDocument.Parse(xmlfile.text);

        //this var will contain all the elements under the "npc" tag
        var allDict = doc.Element("root").Elements("npc");


        //Access each "npc" tag in the xml document
        foreach (var oneDict in allDict)
        {
            //Get the name of the npc in the current oneDict
            string name = (string)oneDict.Attribute("name");

            //Contains all the Elements under the "dialogue" tag
            var allXDialogues = oneDict.Elements();

            List<Dialogue> dialogues = new List<Dialogue>();

            //Access each "dialouge" tag in the xml document
            foreach (XElement xDialogue in allXDialogues)
            {
                //Contains all the Elements under the "sentence" tag
                var allXSentences = xDialogue.Elements();

                //List of strings. The values of the elements under the "sentence" tag will be stored in this list.
                List<string> sentences = new List<string>();

                //Access each "sentence" tag in the xml document
                foreach (XElement xSentence in allXSentences)
                {
                    //add the value of each sentence in the sentences list and ensure that the open and close tags are excluded.
                    sentences.Add(xSentence.ToString().Replace("<sentence>", "").Replace("</sentence>", ""));
                }
                //Create a new dialogue to reference the current dialogue being iterated over
                Dialogue dialogue = new Dialogue();

                //Add all the sentences to the dialogue's sentences list
                dialogue.sentences = sentences;

                //Add this dialogue to the list of dialogues
                dialogues.Add(dialogue);
            }

            //Instantiate a new npc with the name and dialogues created in the inner loops
            NPC npc = new NPC(name, dialogues);

            //Add the newly created npc to our Dictionary and List.
            npcs.Add(npc.name, npc);
            npcList.Add(npc);

            
            
        }
    }

}
