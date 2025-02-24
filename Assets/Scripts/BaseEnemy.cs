using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


enum EnemyState
{
    Idle = 0,
    Patrol = 1,
    Chase = 2,
    Attack = 3,
}

public class BaseEnemy : MonoBehaviour
{

    [SerializeField] private EnemyState currentState; //enemy current state

    [SerializeField] private EnemyState baseState; // the state the enemy returns to once no longer chasing or attacking, always Idle or Patrol

    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float chaseRange = 10f; // if the player is within this range the enemy will chase them if they are in the chase state 

    [SerializeField] private float movementSpeed = 4f; // maybe one for chase speed as well?
    [SerializeField] private List<PatrolPoint> patrolPoints;
    // points to patrol between, make a function that makes the enemy go towards a one of these points, in order,
    // then when reached, move to the next point, then when the end of the patrol is reached, move backwards up the list

    private PatrolPoint currentPatrolPoint; // the patrol point the enemy is currently moving towards
    private int currentPatrolIndex; // index of current patrol point

    [SerializeField] private float patrolDistance = 0.1f; // when within this distance of a patrol point, move on to the next one

    private bool patrolBackward; // which direction are we going

    [SerializeField]private bool cicularPatrol; // if true, when the patrol reaches it's end, it will go to the first index instead of go reversed

    private float waitTime;

    [SerializeField] private float startWaitTime = 0.5f;



    void Start()
    {
        waitTime = startWaitTime;
        currentPatrolPoint = patrolPoints[0];
        currentPatrolIndex = 0;
    }

    // Update is called once per frame 
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                break;

            case EnemyState.Patrol:
                 Patrol();
                break;

            case EnemyState.Chase:
                //implement
                break;

            case EnemyState.Attack:
                break;
        }
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPatrolPoint.transform.position, movementSpeed * Time.deltaTime);

        if ((transform.position - currentPatrolPoint.myPosition).magnitude < patrolDistance)
        {

            if (waitTime <= 0)
            {
                if (cicularPatrol)
                {
                    currentPatrolIndex++;
                    if (currentPatrolIndex >= patrolPoints.Count)
                    {
                        currentPatrolIndex = 0;
                    }
                }
                else if (!cicularPatrol)
                {
                    if (currentPatrolIndex == patrolPoints.Count - 1)
                    {
                        currentPatrolIndex--;
                        patrolBackward = true;

                    }
                    else if (currentPatrolIndex == 0)
                    {
                        currentPatrolIndex++;
                        patrolBackward = false;

                    }
                    else if (patrolBackward)
                    {
                        currentPatrolIndex--;
                    }
                    else if (!patrolBackward)
                    {
                        currentPatrolIndex++;
                    }
                }
                currentPatrolPoint = patrolPoints[currentPatrolIndex];

                waitTime = startWaitTime;

            }
            else waitTime -= Time.deltaTime;
        }
    }
}
