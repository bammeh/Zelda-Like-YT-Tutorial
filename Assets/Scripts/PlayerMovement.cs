using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState // State Machine
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0); // For down
        animator.SetFloat("moveY", -1); // For down
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero; // Set change to 0 each frame.
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");  
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f); // length of attack anim is .3f
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    //Seperate method to move character from other places
    void MoveCharacter()
    {
        change.Normalize(); // Normalize the speed so we dont move 2x as fast diagonally.
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime); //Apply movement on the character. 
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.initialValue -= damage; // Deal damage
        if (currentHealth.initialValue > 0) //Make sure we arent dead...
        {
            playerHealthSignal.Raise(); // Alert everything know we got hit.
            StartCoroutine(KnockCo(knockTime));
        }

    }

    private IEnumerator KnockCo( float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime); // Sets max time for knock to occur before starts chargin towards player again.
            myRigidbody.velocity = Vector2.zero;//Stop player from moving.
            currentState = PlayerState.idle; //set player back to idle
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
