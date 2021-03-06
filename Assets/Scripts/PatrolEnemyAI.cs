﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyAI : MonoBehaviour
{

    public Transform[] patrolpoints;
    int currentPoint;
    public float speed = 0.1f;
    public float timestill = 1f;
    public float sight = 2f;
    public float force;
    public int health = 16;

    public static bool isPaused;

    public bool attacking;
    public bool destroy;
    public bool preventMovement;


    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine("Patrol");

        Physics2D.queriesStartInColliders = false;


    }

    // Update is called once per frame
    void Update()
    {

        //test take damage animation
        if (Input.GetKeyDown("2")){
            speed = 0;
            anim.Play("soldierDamage");
            // anim.SetTrigger("takeDamage");

        }
            

        if (isPaused == false)
        {
            // Set up line of sight detection
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, sight);

            // Move towards player if player in hit Raycast
            if (hit.collider != null && hit.collider.tag == "Player")
            {

                //GetComponent<Rigidbody2D>().AddForce(Vector3.up * force + (hit.collider.transform.position - transform.position) * force);
                attacking = true;

                if (attacking == true)
                {
                    preventMovement = true;
                    anim.SetTrigger("PatrolAttack");

                }


            }

            if (preventMovement == false){
                speed = 0.1f;
                attacking = false;
            }else{
                speed = 0;
            }
         

            // Check health
            if (health <= 0)
            {
                if(destroy == true){
                    Destroy(this.gameObject, 0.1f);
                }

            }
        }

    }

    // Patrol code
    IEnumerator Patrol()
    {
        while (true && isPaused == false)
        {


            // If at a patrol point, wait and set next patrol point
            if (transform.position.x == patrolpoints[currentPoint].position.x)
            {
                currentPoint++;
                anim.SetTrigger("stopWalking");
                yield return new WaitForSeconds(timestill);
                anim.SetTrigger("startWalk");
            }

            // If at the last patrol point, reset patrol point index
            if (currentPoint >= patrolpoints.Length)
            {
                currentPoint = 0;
            }



            // Setting move direction and "facing" position
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(patrolpoints[currentPoint].position.x, transform.position.y), speed);

            if (transform.position.x > patrolpoints[currentPoint].position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x < patrolpoints[currentPoint].position.x)
            {
                transform.localScale = Vector3.one;
            }


            yield return null;
        }
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Attack")
        {
            anim.SetTrigger("takeDamage");
            health -= 8;
        }
    }



    // Show line of sight
    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * sight);
    }

}
