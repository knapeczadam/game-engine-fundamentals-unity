using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public class PlayerCharacter : BasicCharacter
{
    [SerializeField] 
    private InputActionAsset _inputAsset;
    
    [SerializeField]
    private InputActionReference _movementAction;
    
    [SerializeField]
    private InputActionReference _runAction;
    
    [SerializeField]
    private InputActionReference _attackAction;
    
    [SerializeField]
    private InputActionReference _pickUpAction;

    [SerializeField] 
    private InputActionReference _switchWeaponAction;
    
    private PickUpBehaviour _pickUpBehaviour;
    private SwitchWeaponBehaviour _switchWeaponBehaviour;

    protected override void Awake()
    {
        base.Awake();
        
        if (_inputAsset == null)
        {
            Debug.LogError("Input Asset is not set in the PlayerCharacter component.");
            return;
        }
        
        _pickUpBehaviour = GetComponent<PickUpBehaviour>();
        _switchWeaponBehaviour = GetComponent<SwitchWeaponBehaviour>();
    }

    private void OnEnable()
    {
        if (_inputAsset)
        {
            _inputAsset.Enable();
        }

        if (_pickUpAction)
        {
            _pickUpAction.action.performed += HandlePickUpInput;
        }
        
        if (_switchWeaponAction)
        {
            _switchWeaponAction.action.performed += HandleSwitchWeaponInput;
        }
    }
    
    private void OnDisable()
    {
        if (_inputAsset)
        {
            _inputAsset.Disable();
        }

        if (_pickUpAction)
        {
            _pickUpAction.action.performed -= HandlePickUpInput;
        }
        
        if (_switchWeaponAction)
        {
            _switchWeaponAction.action.performed -= HandleSwitchWeaponInput;
        }
    }


    private void Update()
    {
        HandleMovementInput();
        HandleAttackInput();
    }

    private void HandleMovementInput()
    {
        if (_movementBehaviour == null || _movementAction == null)
        {
            Debug.LogError("MovementBehaviour or Movement Action is not set in the PlayerCharacter component.");
            return;
        }
        
        Vector2 movement = _movementAction.action.ReadValue<Vector2>();
        Vector3 movementDirection = new Vector3(movement.x, 0, movement.y);
        _movementBehaviour.DesiredMovementDirection = movementDirection;
        
        _movementBehaviour.IsRunning = _runAction.action.IsPressed();
    }
    
    private void HandleAttackInput()
    {
        if (_attackBehaviour == null || _attackAction == null)
        {
            Debug.LogError("AttackBehaviour or Attack Action is not set in the PlayerCharacter component.");
            return;
        }
        
        if (_attackAction.action.IsPressed())
        {
            _attackBehaviour.Attack();
        }
    }

    private void HandlePickUpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _pickUpBehaviour.PickUp();
        }
    }
    
    private void HandleSwitchWeaponInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Values start from 1, so we need to subtract 1 to get the correct index
            var weaponIndex = (int) context.ReadValue<float>();
            _switchWeaponBehaviour.SwitchWeapon(weaponIndex - 1);
        }
    }

}
