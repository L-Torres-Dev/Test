using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

    private Rigidbody2D myBody;

    private float health = 10f;
    private int dir;

    public float speed;
    private Transform groundCheck1, groundCheck2;

    private RaycastHit2D hit1, hit2;
    private bool hit;
    private bool justHitEdge;

    private void Start()
    {
        dir = 1;
        groundCheck1 = GameObject.Find("EnemyGroundCheck").GetComponent<Transform>();
        groundCheck2 = GameObject.Find("EnemyGroundCheck2").GetComponent<Transform>();
        myBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.WALL_TAG)
        {
            dir *= -1;
        }
    }

    private void Update()
    {
        Grounded();
    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dir, 0);
    }

    private void Grounded()
    {
        //The starting position of the raycast which will be at the player's GroundCheck Object (At the player's feet)
        Vector2 startingPosition = groundCheck1.transform.position;
        Vector2 startingPosition2 = groundCheck2.transform.position;

        //Casts the ray downward, the ray is a miniscule length of .1 units so that it only detects colliders close to
        //the player's feet
        hit1 = Physics2D.Raycast(startingPosition, new Vector2(0, -1), .1f);
        hit2 = Physics2D.Raycast(startingPosition2, new Vector2(0, -1), .1f);

        hit = (hit1 || hit2) && myBody.velocity.y == 0;

        if (((!hit1 && hit2) || (hit1 && !hit2)) && !justHitEdge)
        {
            dir *= -1;
            justHitEdge = false;
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator JustHitEdge()
    {
        yield return new WaitForSeconds(.1f);
        justHitEdge = true;
    }
}
