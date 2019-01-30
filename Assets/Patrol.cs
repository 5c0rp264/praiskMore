using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Patrol : MonoBehaviour
{
    public enum PatrolMode
    {
        Loop,
        Reverse
    };

    // Variables
    public Transform[] patrolPoints;
    public PatrolMode patrolMode;
    public float speedRatio = 5f;

    // Internal
    protected int destPoint = 0;
    protected int direction = 1;

    // Components
    protected NavMeshAgent agent;
    protected Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (patrolPoints.Length == 0)
        {
            Debug.LogError("no patrol points defined");
        }
        else
        {
            agent.SetDestination(patrolPoints[destPoint].position);
        }
    }

    void FixedUpdate()
    {
        float speed = agent.velocity.magnitude / speedRatio;
        speed = Mathf.Clamp01(speed);
        anim.SetFloat("speed", speed);

        if (patrolPoints.Length > 0)
        {
            // if we reach the destination
            if (Vector3.Distance(transform.position, patrolPoints[destPoint].position) < 0.2f)
            {
                // find a new destination
                NextPatrolPoint();
                agent.SetDestination(patrolPoints[destPoint].position);
            }
        }
    }

    void NextPatrolPoint()
    {
        switch (patrolMode)
        {
            case PatrolMode.Loop:
                Loop();
                break;
            case PatrolMode.Reverse:
                Reverse();
                break;
        }
    }

    void Loop()
    {
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }

    void Reverse()
    {
        // if we reach one end
        // change direction
        if (destPoint == 0)
            direction = 1;
        else if (destPoint == patrolPoints.Length - 1)
            direction = -1;

        destPoint = destPoint + direction;
    }
}
