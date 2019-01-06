using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApprenticeSpell : MonoBehaviour {

    private GameObject player;
    private InputProcessor inputProcessor;

    public float damage;
    public float dmgTimer;
    public float lifeTime;
    

    private bool canDamage;
   

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        inputProcessor = player.GetComponent<InputProcessor>();

        StartCoroutine(LifeTime());

    }

    
  

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag != Tags.PLAYER_TAG && collision.tag != Tags.ENEMY_TAG)
        {
            gameObject.SetActive(false);
        }

        if (collision.tag == Tags.ENEMY_TAG && canDamage)
        {
            collision.GetComponent<BasicEnemy>().TakeDamage(doDamage());
            canDamage = false;
            StartCoroutine(JustDamaged());
        }

    }



    private float doDamage()
    {
        return damage;
    }

    


    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
    }

    IEnumerator JustDamaged()
    {
        yield return new WaitForSeconds(dmgTimer);
        canDamage = true;
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
