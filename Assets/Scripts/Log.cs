using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy // Transfers inheritance from Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform; // Gets player location to move towards.
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() // 30 times per second. only physics calls
    {
        CheckDistance();
    }
    //Gets distance from log to target.
    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position,transform.position) > attackRadius) // If our distance from player is lessthan or equal to chase radius.
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);//If in range, walk.
            }

        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (newState != currentState) //so we dont constantly change states if not needed.
        {
            currentState = newState;
        }
    }
}
