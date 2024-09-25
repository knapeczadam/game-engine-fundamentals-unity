using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovementBehaviour : MovementBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Vector3 _previousTargetPosition = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _movementSpeed;

        _previousTargetPosition = transform.position;
    }
    
    const float MOVEMENT_THRESHOLD = 0.25f;
    protected override void HandleMovement()
    {
        if (_target is null)
        {
            _navMeshAgent.isStopped = true;
            return;
        }
        
        if ((_target.transform.position - _previousTargetPosition).sqrMagnitude > MOVEMENT_THRESHOLD * MOVEMENT_THRESHOLD)
        {
            _navMeshAgent.SetDestination(_target.transform.position);
            _navMeshAgent.isStopped = false;
            _previousTargetPosition = _target.transform.position;
        }
    }
}
