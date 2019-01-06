using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedSpells : MonoBehaviour {

    private GameObject primarySpell;
    private GameObject secondarySpell;
    private GameObject standardSpell;
    private GameObject signatureSpell;
    
    // Use this for initialization
    void Awake () {
        primarySpell = GameObject.FindGameObjectWithTag(Tags.SPELLSMANAGER_TAG).GetComponent<SpellsManager>().primarySpells[0];
        secondarySpell = GameObject.FindGameObjectWithTag(Tags.SPELLSMANAGER_TAG).GetComponent<SpellsManager>().secondarySpells[0];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject PrimarySpell
    {
        get
        {
            return primarySpell;
        }

        set
        {
            primarySpell = value;
        }
    }

    public GameObject SecondarySpell
    {
        get
        {
            return secondarySpell;
        }

        set
        {
            secondarySpell = value;
        }
    }

    public GameObject StandardSpell
    {
        get
        {
            return standardSpell;
        }

        set
        {
            standardSpell = value;
        }
    }

    public GameObject SignatureSpell
    {
        get
        {
            return signatureSpell;
        }

        set
        {
            signatureSpell = value;
        }
    }
}
