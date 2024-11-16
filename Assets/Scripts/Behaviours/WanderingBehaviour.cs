using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace GEF
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class WanderingBehaviour : MovementBehaviour
    {
        #region Properties
        [SerializeField, Range(0.01f, 20.0f)] private float m_wanderRadius = 10;
        private NavMeshAgent m_navMeshAgent = null;
        #endregion

        #region Lifecycle
        protected override void Awake()
        {
            base.Awake();
            m_navMeshAgent = GetComponent<NavMeshAgent>();
            m_navMeshAgent.speed = m_movementSpeed;
        }

        private void OnEnable()
        {
            m_navMeshAgent.isStopped = false;
            CalculateNewDestination();
        }

        private void Start()
        {
            m_target = new GameObject("Wandering Target");
            CalculateNewDestination();
        }
        #endregion

        #region Methods

        protected override void HandleMovement()
        {
            if (m_target == null)
            {
                m_navMeshAgent.isStopped = true;
                return;
            }

            if ((m_target.transform.position - transform.position).sqrMagnitude < 10)
            {
                CalculateNewDestination();
            }
        }
        
        private void CalculateNewDestination()
        {
            Vector3 randomDirection = Random.insideUnitSphere * m_wanderRadius;
            randomDirection += transform.position;

            NavMesh.SamplePosition(randomDirection, out var hit, m_wanderRadius, 1);
            Vector3 finalPosition = hit.position;

            if (m_target)
            {
                m_target.transform.position = finalPosition;
            }

            m_navMeshAgent.SetDestination(finalPosition);
        }
        #endregion
    }
}