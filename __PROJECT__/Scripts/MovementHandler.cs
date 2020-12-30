using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
using Pathfinding;

public enum AIState { IDLE=0, PATROL=1, SEARCH=2, ATTACK=3 }
public class MovementHandler : MonoBehaviour
{
    [ShowInInspector]
    public AIState State;
    public bool Loop;
    private Seeker seeker;
    private AIPath ai;
    
    private PatrolPoints patrolPoints;
    public bool usePatrolPoints = false;

    private bool destinationReached = false;
    private int currentPoint = 0;

    private GameObject player;

    void Start()
    {
        patrolPoints = GetComponent<PatrolPoints>();
        seeker = GetComponent<Seeker>();
        ai = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player");

        DeterminePath();
        StartCoroutine(DetermineDestinationReached());
    }

    void DeterminePath()
    {
        if (State == AIState.IDLE)
        {
            State = AIState.PATROL;
            if (usePatrolPoints)
                ai.destination = patrolPoints._PatrolPoints[currentPoint].transform.position;
        }
    }

    void DetermineAttack()
    {
        RaycastHit2D rh;
        if (rh = Physics2D.Raycast(transform.position, player.transform.position-transform.position, 100f, ~LayerMask.GetMask("Enemy")))
        {
            if (rh.collider.tag == "Player")    //Player Visible
            {
                State = AIState.ATTACK; //Prioritize attack over patrol and give control back to the coroutine
                return;
            }
            else 
            {
                State = AIState.IDLE;
            }
        }
    }

    void DetermineAttackPath()
    {
        ai.destination = player.transform.position;
    }

    IEnumerator DetermineDestinationReached()
    {
        while (true)
        {
            DetermineAttack();
            if (State == AIState.ATTACK)
            {
                DetermineAttackPath();
            }
            else if (ai.reachedDestination && State != AIState.ATTACK && usePatrolPoints)
            {
                ++currentPoint;
                State = AIState.IDLE;

                if (patrolPoints._PatrolPoints.Count == currentPoint && Loop)
                    currentPoint = 0;
                if (currentPoint < patrolPoints._PatrolPoints.Count)
                    DeterminePath();
            }
            else if (State == AIState.IDLE)
            {
                DeterminePath();
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
