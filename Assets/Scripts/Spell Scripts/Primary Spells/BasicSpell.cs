using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpell : MonoBehaviour {

    private GameObject player;
    private InputProcessor inputProcessor;

    public float damage;
    
    public float lifeTime;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        inputProcessor = player.GetComponent<InputProcessor>();
        
        StartCoroutine(LifeTime());
        
    }

    private void Update()
    {

    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag != Tags.PLAYER_TAG)
        {
            gameObject.SetActive(false);
        }

        if (collision.tag == Tags.ENEMY_TAG)
        {
            collision.GetComponent<BasicEnemy>().TakeDamage(doDamage());
        }

    }

    private float doDamage()
    {
        return damage;
    }

    public float Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }
}
