using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PetState // State Machine
{
    walk,
    idle
}
public class PetMovement : MonoBehaviour
{
    public PetState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public GameObject player;
    public VectorValue startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0); // For down
        animator.SetFloat("moveY", -1); // For down
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero; // Set change to 0 each frame.
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (currentState == PetState.walk || currentState == PetState.idle)
        {
            UpdateAnimationAndMove();
        }
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
    void MoveCharacter()
    {
        var petOffsetX = change.x == 0 ?
            player.transform.position.x :
            change.x > 0 ? player.transform.position.x - (float)1.2 : player.transform.position.x + (float)1.2;
        var petOffsetY = change.y == 0 ?
            player.transform.position.y :
            change.y > 0 ? player.transform.position.y - (float)1.2 : player.transform.position.y + (float)1.2;

        Vector3 petOffset = new Vector3(petOffsetX, petOffsetY, transform.position.z);
        change.Normalize(); // Normalize the speed so we dont move 2x as fast diagonally.
        var newPos = player.transform.position;
        transform.position += ((newPos - transform.position) * 0.03f);
        //myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime); //Apply movement on the character.
    }

    void OnCollisionEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            myRigidbody.isKinematic = true;
        }
    }
}
