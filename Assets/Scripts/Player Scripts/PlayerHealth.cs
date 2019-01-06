using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public Slider healthBar;

    private float maxHealth = 10;
    private float currentHealth = 10;

    private bool justTookDamge;
    private bool knockBack;
    private bool enemyRightSide;
    private bool canTakeDamage;
    public float damageTimer;

    Animator anim;

    

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        knockBack = false;
        canTakeDamage = true;

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.ENEMY_TAG)
        {
            if (canTakeDamage)
            {
                TakeDmg(4);
                canTakeDamage = false;
            }
            
            if (collision.GetComponent<Transform>().position.x > gameObject.GetComponent<Transform>().position.x)
            {
                enemyRightSide = true;
            }

            else
            {
                enemyRightSide = false;
            }

        }
    }

    private void TakeDmg(float dmg)
    {
        currentHealth -= dmg;
        justTookDamge = true;
        knockBack = true;

        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }

        anim.SetLayerWeight(1, 1);
        Physics2D.IgnoreLayerCollision(8, 9);
        StartCoroutine(JustTookDamge());
    }

    private void Heal(float healing)
    {
        currentHealth += healing;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.value = currentHealth;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    IEnumerator JustTookDamge()
    {
        yield return new WaitForSeconds(damageTimer);

        justTookDamge = false;
        anim.SetLayerWeight(1, 0);
        canTakeDamage = true;
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    public bool JustTookDamge1
    {
        get
        {
            return justTookDamge;
        }

        set
        {
            justTookDamge = value;
        }
    }

    public bool KnockBack
    {
        get
        {
            return knockBack;
        }

        set
        {
            knockBack = value;
        }
    }

    public bool EnemyRightSide
    {
        get
        {
            return enemyRightSide;
        }

        set
        {
            enemyRightSide = value;
        }
    }

    public bool CanTakeDamage
    {
        get
        {
            return canTakeDamage;
        }

        set
        {
            canTakeDamage = value;
        }
    }
}
