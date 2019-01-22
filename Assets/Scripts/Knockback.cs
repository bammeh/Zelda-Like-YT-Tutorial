using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust; // How much force should we knockback with?
    public float knockTime;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))// is this an enemy?
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                enemy.isKinematic = false; 
                Vector2 difference = enemy.transform.position - transform.position; // get difference between my center point and enemy centerpoint
                difference = difference.normalized * thrust; // Turing in vector with length of 1.
                enemy.AddForce(difference, ForceMode2D.Impulse); // add instant force to enemy.
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime); // Sets max time for knock to occur before starts chargin towards player again.
            enemy.velocity = Vector2.zero;//Stop enemy from moving.
            enemy.isKinematic = true; //Stops it from being able to push player around.
        }
    }
}
