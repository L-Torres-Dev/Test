using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC {

    public string name;
    public List<Dialogue> dialogues;


    public NPC(string name, List<Dialogue> dialogues)
    {
        this.name = name;
        this.dialogues = dialogues;

    }
}
