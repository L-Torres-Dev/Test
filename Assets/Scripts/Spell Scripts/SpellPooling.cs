using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPooling : MonoBehaviour {

    GameObject player;
    EquippedSpells equippedSpells;

    private List<List<GameObject>> spells;                  //List of the equipped spells

    private GameObject primarySpell;                        //Spell GameObject that represents the basic spell slot
    private GameObject secondarySpell;
    private GameObject standardSpell;
    private GameObject signatureSpell;

    private List<GameObject> primarySpells;                   //Pooled primary spells
    private List<GameObject> secondarySpells;                 //Pooled Secondary spells
    private List<GameObject> standardSpells;                  //Pooled Standard spells
    private List<GameObject> signatureSpells;                 //Pooled Signature spells

    private int[] queueIndexes;
    private int primarySpellQueueIndex;                       //primary spell queue
    private int secondarySpellQueueIndex;                     //secondary spell queue
    private int standardSpellQueueIndex;                      //standard spell queue
    private int signatureSpellQueueIndex;                     //signature spell queue


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        equippedSpells = player.GetComponent<EquippedSpells>();
        primarySpells = new List<GameObject>();
        secondarySpells = new List<GameObject>();
        standardSpells = new List<GameObject>();
        signatureSpells = new List<GameObject>();
        spells = new List<List<GameObject>>();
        spells.Add(primarySpells);
        spells.Add(secondarySpells);
        spells.Add(standardSpells);
        spells.Add(signatureSpells);

        primarySpellQueueIndex = 0;
        secondarySpellQueueIndex = 0;
        standardSpellQueueIndex = 0;
        signatureSpellQueueIndex = 0;

        queueIndexes = new int[4];
        queueIndexes[0] = primarySpellQueueIndex;
        queueIndexes[1] = secondarySpellQueueIndex;
        queueIndexes[2] = standardSpellQueueIndex;
        queueIndexes[3] = signatureSpellQueueIndex;

        InstantiateSpells();
        
	}
	
	public GameObject PoolSpell(int spellIndex)
    {
        List<GameObject> spellKind = spells[spellIndex];

        GameObject pooledSpell = spellKind[queueIndexes[spellIndex]++];

        if (queueIndexes[spellIndex] == spellKind.Count)
        {
            queueIndexes[spellIndex] = 0;
        }
        return pooledSpell;
    }

    private void InstantiateSpells()
    {
        primarySpell = equippedSpells.PrimarySpell;
        secondarySpell = equippedSpells.SecondarySpell;

        //Instantiate primary spells
        for (int i = 0; i < 15; i++)
        {
            GameObject spell = Instantiate(primarySpell, GameObject.Find("SpellShoot").transform.position, Quaternion.identity);
            spell.transform.SetParent(GameObject.Find("Spells").transform);
            spell.SetActive(false);

            primarySpells.Add(spell);
        }

        //Instantiat Secondary spells
        for (int i = 0; i < 10; i++)
        {
            GameObject spell = Instantiate(secondarySpell, GameObject.Find("SpellShoot").transform.position, Quaternion.identity);
            spell.transform.SetParent(GameObject.Find("Spells").transform);
            spell.SetActive(false);

            secondarySpells.Add(spell);
        }
    }

    public GameObject BasicSpell
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
}
