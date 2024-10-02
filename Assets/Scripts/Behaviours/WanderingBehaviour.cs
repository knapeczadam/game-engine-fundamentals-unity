using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class WanderingBehaviour : MovementBehaviour
{
    [SerializeField]
    private float _wanderRadius = 10;
    
    private NavMeshAgent _navMeshAgent;
    protected override void Awake()
    {
        base.Awake();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _movementSpeed;
    }
    
    private void Start()
    {
        Target = new GameObject("Wandering Target");
        CalculateNewDestination();
    }
    
    private void CalculateNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMeshSurface surface = FindObjectOfType<NavMeshSurface>();
        NavMesh.SamplePosition(randomDirection, out hit, _wanderRadius, 1);
        Vector3 finalPosition = hit.position;
        _target.transform.position = finalPosition;
        _navMeshAgent.SetDestination(finalPosition);
    }
    
    protected override void HandleMovement()
    {
        if ((_target.transform.position - transform.position).sqrMagnitude < 10)
        {
            CalculateNewDestination();
        }
    }
}
