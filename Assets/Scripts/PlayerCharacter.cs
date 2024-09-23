using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : BasicCharacter
{
    [SerializeField] 
    private InputActionAsset _inputAsset;

    [SerializeField] 
    private InputActionReference _horizontalMovementAction;
    
    [SerializeField]
    private InputActionReference _verticalMovementAction;
    
    [SerializeField]
    private InputActionReference _attackAction;
    
    [SerializeField]
    private InputActionReference _rescueAction;

    protected override void Awake()
    {
        base.Awake();
        
        if (_inputAsset is null)
        {
            Debug.LogError("Input Asset is not set in the PlayerCharacter component.");
            return;
        }
    }

    private void OnEnable()
    {
        if (_inputAsset is object)
        {
            _inputAsset.Enable();
        }
    }
    
    private void OnDisable()
    {
        if (_inputAsset is object)
        {
            _inputAsset.Disable();
        }
    }

    private void Update()
    {
        HandleHorizontalMovementInput();
        HandleVerticalMovementInput();
        HandleAttackInput();
    }

    void HandleHorizontalMovementInput()
    {
        if (_movementBehaviour is null || _horizontalMovementAction is null)
        {
            Debug.LogError("MovementBehaviour or Horizontal Movement Action is not set in the PlayerCharacter component.");
            return;
        }
        
        float horizontalMovement = _horizontalMovementAction.action.ReadValue<float>();
        Vector3 movement  = horizontalMovement * Vector3.right;
        _movementBehaviour.DesiredHorizontalMovementDirection = movement;
    }
    
    void HandleVerticalMovementInput()
    {
        if (_movementBehaviour is null || _verticalMovementAction is null)
        {
            Debug.LogError("MovementBehaviour or Vertical Movement Action is not set in the PlayerCharacter component.");
            return;
        }
        
        float verticalMovement = _verticalMovementAction.action.ReadValue<float>();
        Vector3 movement  = verticalMovement * Vector3.forward;
        _movementBehaviour.DesiredVerticalMovementDirection = movement;
    }
    
    private void HandleAttackInput()
    {
        if (_attackBehaviour is null || _attackAction is null)
        {
            Debug.LogError("AttackBehaviour or Attack Action is not set in the PlayerCharacter component.");
            return;
        }
        
        if (_attackAction.action.IsPressed())
        {
            _attackBehaviour.Attack();
        }
        
    }
    
    private void HandleRescue()
    {
    }
}
