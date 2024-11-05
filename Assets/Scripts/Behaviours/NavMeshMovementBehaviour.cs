using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), DisallowMultipleComponent]
public class NavMeshMovementBehaviour : MovementBehaviour
{
    private NavMeshAgent m_navMeshAgent           = null;
    private Vector3      m_previousTargetPosition = Vector3.zero;
    private const float  m_MOVEMENT_THRESHOLD      = 0.25f;

    protected override void Awake()
    {
        base.Awake();
        
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.speed = m_movementSpeed;

        m_previousTargetPosition = transform.position;
    }
    
    protected override void HandleMovement()
    {
        if (m_target == null)
        {
            m_navMeshAgent.isStopped = true;
            return;
        }
        
        if ((m_target.transform.position - m_previousTargetPosition).sqrMagnitude > m_MOVEMENT_THRESHOLD * m_MOVEMENT_THRESHOLD)
        {
            m_navMeshAgent.SetDestination(m_target.transform.position);
            m_navMeshAgent.isStopped = false;
            m_previousTargetPosition = m_target.transform.position;
        }
    }
}
