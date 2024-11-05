using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public class PlayerCharacter : BasicCharacter
{
    [SerializeField] private InputActionAsset     m_inputAsset         = null;
    [SerializeField] private InputActionReference m_movementAction     = null;
    [SerializeField] private InputActionReference m_runAction          = null;
    [SerializeField] private InputActionReference m_attackAction       = null;
    [SerializeField] private InputActionReference m_pickUpAction       = null;
    [SerializeField] private InputActionReference m_switchWeaponAction ;
    [SerializeField] private InputActionReference m_cameraDistanceAction       = null;  
    [SerializeField] private InputActionReference m_cameraRotationAction = null;
    [SerializeField] private CameraManager        m_cameraManager       = null;
    
    private PickUpBehaviour       m_pickUpBehaviour       = null;
    private SwitchWeaponBehaviour m_switchWeaponBehaviour = null;
    private WeaponManager         m_weaponManager         = null;

    protected override void Awake()
    {
        base.Awake();
        
        if (m_inputAsset == null)
        {
            Debug.LogError("Input Asset is not set in the PlayerCharacter component.");
            return;
        }
        
        m_pickUpBehaviour       = GetComponent<PickUpBehaviour>();
        m_switchWeaponBehaviour = GetComponent<SwitchWeaponBehaviour>();
        m_weaponManager         = GetComponent<WeaponManager>();
    }

    private void OnEnable()
    {
        if (m_inputAsset)
        {
            m_inputAsset.Enable();
        }

        if (m_pickUpAction)
        {
            m_pickUpAction.action.performed += HandlePickUpInput;
        }
        
        if (m_switchWeaponAction)
        {
            m_switchWeaponAction.action.performed += HandleSwitchWeaponInput;
        }
        
        if (m_cameraDistanceAction)
        {
            m_cameraDistanceAction.action.performed += HandleCameraDistanceInput;
        }
    }
    
    private void OnDisable()
    {
        if (m_inputAsset)
        {
            m_inputAsset.Disable();
        }

        if (m_pickUpAction)
        {
            m_pickUpAction.action.performed -= HandlePickUpInput;
        }
        
        if (m_switchWeaponAction)
        {
            m_switchWeaponAction.action.performed -= HandleSwitchWeaponInput;
        }
        
        if (m_cameraDistanceAction)
        {
            m_cameraDistanceAction.action.performed -= HandleCameraDistanceInput;
        }
    }


    private void Update()
    {
        HandleMovementInput();
        HandleAttackInput();
        HandleCameraRotationInput();
    }

    private void HandleMovementInput()
    {
        if (m_movementBehaviour == null || m_movementAction == null)
        {
            Debug.LogError("MovementBehaviour or Movement Action is not set in the PlayerCharacter component.");
            return;
        }
        
        Vector2 movement = m_movementAction.action.ReadValue<Vector2>();
        Vector3 movementDirection = new Vector3(movement.x, 0, movement.y);
        m_movementBehaviour.m_desiredMovementDirection = movementDirection;
        
        m_movementBehaviour.m_runPressed = m_runAction.action.IsPressed();
    }
    
    private void HandleAttackInput()
    {
        if (m_attackBehaviour == null || m_attackAction == null)
        {
            Debug.LogError("AttackBehaviour or Attack Action is not set in the PlayerCharacter component.");
            return;
        }

        if (m_weaponManager.m_currentWeapon.GetComponent<BasicWeapon>().m_fastFire)
        {
            if (m_attackAction.action.IsPressed())
            {
                m_attackBehaviour.Attack();
            }
        }
        else
        {
            if (m_attackAction.action.WasPerformedThisFrame())
            {
                m_attackBehaviour.Attack();
            }
        }
    }

    private void HandlePickUpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_pickUpBehaviour.PickUp();
        }
    }
    
    private void HandleSwitchWeaponInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Values start from 1, so we need to subtract 1 to get the correct index
            var weaponIndex = (int) context.ReadValue<float>();
            m_switchWeaponBehaviour.SwitchWeapon(weaponIndex - 1);
        }
    }
    
    private void HandleCameraDistanceInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log(context.ReadValue<float>());
            var value = context.ReadValue<float>();
            if (value > 0)
            {
                m_cameraManager.DecreaseCameraDistance();
            }
            else if (value < 0)
            {
                m_cameraManager.IncreaseCameraDistance();
            }
        }
    }
    
    private void HandleCameraRotationInput()
    {
        if (m_cameraRotationAction.action.IsPressed())
        {
            var value = m_cameraRotationAction.action.ReadValue<float>();
            if (value > 0)
            {
                m_cameraManager.RotateCameraRight();
            }
            else if (value < 0)
            {
                m_cameraManager.RotateCameraLeft();
            }
        }
    }

}
