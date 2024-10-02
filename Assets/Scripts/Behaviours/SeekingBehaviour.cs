using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SeekingBehaviour : MovementBehaviour
{
    private NavMeshAgent _navMeshAgent;
    
    protected override void Awake()
    {
        base.Awake();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _movementSpeed;
    }

    const float MOVEMENT_THRESHOLD = 0.25f;
    protected override void HandleMovement()
    {
        if ((_target.transform.position - transform.position).sqrMagnitude > MOVEMENT_THRESHOLD)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_target.transform.position);
        }
        else
        {
            _navMeshAgent.isStopped = true;
        }
    }
    
    private void OnEnable()
    {
        _navMeshAgent.isStopped = false;
    }
}
