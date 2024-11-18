using UnityEngine;

namespace GEF
{
    public class MovementBehaviour : MonoBehaviour
    {
        #region Fields
        [HideInInspector] public bool       m_runPressed               = false;
        [HideInInspector] public GameObject m_target                   = null;
        [HideInInspector] public Vector3    m_desiredMovementDirection = Vector3.zero;
        #endregion

        #region Properties
        protected Rigidbody m_rigidbody = null;
        
        [SerializeField, Range(0.01f, 10.0f)] protected float m_movementSpeed = 1.0f;
        [SerializeField, Range(0.01f, 20.0f)] private   float m_rotationSpeed = 10.0f;
        [SerializeField, Range(1.0f,  10.0f)] private   float m_runSpeed      = 2.0f;
        [SerializeField] bool m_infiniteRun = false;
        [SerializeField] private bool m_smoothRotation = true;
        private PickUpBehaviour m_pickUpBehaviour = null;
        private bool m_canRun = true;
        #endregion

        #region Lifecycle
        protected virtual void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_pickUpBehaviour = GetComponent<PickUpBehaviour>(); // TODO: cat doesn't have this component
        }

        private void Start()
        {
            if (m_pickUpBehaviour)
            {
                m_pickUpBehaviour.OnPickUp += HandlePickUp;
            }
        }
        
        protected virtual void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }
        #endregion

        #region Methods
        protected virtual void HandleMovement()
        {
            if (m_rigidbody == null)
            {
                Debug.LogError("Rigidbody is not set in the MovementBehaviour component.");
                return;
            }

            Vector3 movement = m_desiredMovementDirection.normalized;
            float movementSpeed = m_movementSpeed;
            if ((m_canRun && m_runPressed) || (m_canRun && m_infiniteRun))
            {
                movementSpeed *= m_runSpeed;
            }

            if (!m_canRun)
            {
                // movementSpeed *= 0.5f;
            }

            if (!m_canRun && m_runPressed)
            {
                Debug.Log("Cannot run while carrying a cat.");
            }

            movement *= movementSpeed * Time.deltaTime;
            // transform.position += movement;
            m_rigidbody.MovePosition(transform.position + movement);
        }

        protected virtual void HandleRotation()
        {
            if (m_desiredMovementDirection != Vector3.zero)
            {
                if (m_smoothRotation)
                {
                    // Smoothly rotates the character to face the direction of movement.
                    Quaternion targetRotation = Quaternion.LookRotation(m_desiredMovementDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                        m_rotationSpeed * Time.deltaTime);
                }
                else
                {
                    // Rotates the character to face the direction of movement
                    transform.rotation = Quaternion.LookRotation(m_desiredMovementDirection);
                }
            }
        }
        
        private void HandlePickUp(bool catPickedUp)
        {
            m_canRun = !catPickedUp;
        }
        #endregion
    }
}