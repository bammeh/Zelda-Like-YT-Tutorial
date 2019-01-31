using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust; // How much force should we knockback with?
    public float knockTime;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smash(); //Smash pot
        }

        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))// is this an enemy or player?
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>(); // Get Rigidbody
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position; // get difference between my center point and enemy centerpoint
                difference = difference.normalized * thrust; // Turing in vector with length of 1.
                hit.AddForce(difference, ForceMode2D.Impulse); // add instant force to enemy.

                if (other.gameObject.CompareTag("enemy")) // if its an enemy
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger; // set state to stagger
                    other.GetComponent<Enemy>().Knock(hit, knockTime); // begin knockback routine.
                }
                if (other.gameObject.CompareTag("Player")) // if its a player
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;// set state to stagger
                    other.GetComponent<PlayerMovement>().Knock(knockTime); // begin knockback routine.
                }

            }
        }
    }
}
