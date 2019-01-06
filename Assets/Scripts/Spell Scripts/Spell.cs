using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    public Vector2 velocity;
    public float cooldown;
    private bool onCooldown;

    private void Start()
    {
        onCooldown = false;
    }

    public void SetCooldown()
    {
        onCooldown = true;
        StartCoroutine(OnCooldownWait());
    }

    IEnumerator OnCooldownWait()
    {
        yield return new WaitForSeconds(gameObject.GetComponent<Spell>().cooldown);
        onCooldown = false;

    }



    public bool OnCooldown
    {
        get
        {
            return onCooldown;
        }

        set
        {
            onCooldown = value;
        }
    }
}
