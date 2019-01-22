using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState // State Machine
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

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
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk)
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
}
