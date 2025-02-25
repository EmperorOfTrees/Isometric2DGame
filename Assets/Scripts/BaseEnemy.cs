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

    private EnemyState currentState; //enemy current state

    [SerializeField] private EnemyState baseState; // the state the enemy returns to once no longer chasing or attacking, always Idle or Patrol

    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float chaseRange = 10f; // if the player is within this range the enemy will chase them if they are in the chase state
    [SerializeField] private float attackRange = 1f; // if the player is within this range the enemy will attack
    
    private Vector2 playerPosition;
    private float playerDistance;

    [SerializeField] private float movementSpeed = 4f; // maybe one for chase speed as well?
    [SerializeField] private List<PatrolPoint> patrolPoints;

    private PatrolPoint currentPatrolPoint; // the patrol point the enemy is currently moving towards
    private int currentPatrolIndex; // index of current patrol point
    [SerializeField] private Vector2 currentPatrolPointLocation; // a slight random offset for the patrol points, so it doesn't take the exact same path every time

    [SerializeField] private float patrolDistance = 0.1f; // when within this distance of a patrol point, move on to the next one

    private bool patrolBackward; // which direction are we going

    [SerializeField] private bool cicularPatrol; // if true, when the patrol reaches it's end, it will go to the first index instead of go reversed

    private float waitTime;

    [SerializeField] private float startWaitTime = 0.5f;

    void Start()
    {
        currentState = baseState;
        waitTime = startWaitTime;
        currentPatrolPoint = patrolPoints[0];
        RandomisePatrolPointLocation(currentPatrolPoint.myPosition);
        currentPatrolIndex = 0;
        CheckPlayerDistance();
    }

    void Update()
    {
        CheckPlayerDistance();
        StateSwitch();
        StateExecution();
    }

    private void StateExecution()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                break;

            case EnemyState.Patrol:
                 Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;

            case EnemyState.Attack:
                break;
        }
    }

    private void CheckPlayerDistance()
    {
        playerPosition = FindAnyObjectByType<PlayerMovement>().gameObject.transform.position;
        playerDistance = (playerPosition - (Vector2)transform.position).magnitude;

    }

    private void StateSwitch()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                if (playerDistance < detectionRange)
                {
                    currentState = EnemyState.Chase;
                }
                break;

            case EnemyState.Patrol:
                if (playerDistance < detectionRange)
                {
                    currentState = EnemyState.Chase;
                }
                break;

            case EnemyState.Chase:
                if (playerDistance > chaseRange)
                {
                    currentState = baseState;
                }
                if (playerDistance <= attackRange)
                {
                    currentState = EnemyState.Attack;
                }
                break;

            case EnemyState.Attack:
                if (playerDistance > attackRange)
                {
                    currentState = EnemyState.Chase;
                    Debug.Log("Attacking");
                }
                break;
        }
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPatrolPointLocation, movementSpeed * Time.deltaTime);

        if (((Vector2)transform.position - currentPatrolPointLocation).magnitude < patrolDistance)
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
                RandomisePatrolPointLocation(currentPatrolPoint.myPosition);

                waitTime = startWaitTime;

            }
            else waitTime -= Time.deltaTime;
        }
    }

    private void RandomisePatrolPointLocation(Vector3 pPointPosition)
    {
        Vector2 randomChange = new(Random.Range(-1f,1f), Random.Range(-1f, 1f));
        currentPatrolPointLocation = (Vector2)pPointPosition+randomChange;

    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, movementSpeed * Time.deltaTime);
    }
}
