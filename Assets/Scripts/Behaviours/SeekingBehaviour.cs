using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SeekingBehaviour : MovementBehaviour
{
    private NavMeshAgent m_navMeshAgent;
    private const float m_MOVEMENT_THRESHOLD = 0.25f;
    
    protected override void Awake()
    {
        base.Awake();

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.speed = m_movementSpeed;
    }

    protected override void HandleMovement()
    {
        if ((m_target.transform.position - transform.position).sqrMagnitude > m_MOVEMENT_THRESHOLD)
        {
            m_navMeshAgent.isStopped = false;
            m_navMeshAgent.SetDestination(m_target.transform.position);
        }
        else
        {
            m_navMeshAgent.isStopped = true;
        }
    }
    
    private void OnEnable()
    {
        m_navMeshAgent.isStopped = false;
    }
}
