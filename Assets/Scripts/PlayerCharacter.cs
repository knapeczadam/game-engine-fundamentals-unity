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
    private InputActionReference _movementAction;
    
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
        HandleMovementInput();
        HandleAttackInput();
    }
    
    void HandleMovementInput()
    {
        if (_movementBehaviour is null || _movementAction is null)
        {
            Debug.LogError("MovementBehaviour or Movement Action is not set in the PlayerCharacter component.");
            return;
        }
        
        Vector2 movement = _movementAction.action.ReadValue<Vector2>();
        Vector3 movementDirection = new Vector3(movement.x, 0, movement.y);
        _movementBehaviour.DesiredMovementDirection = movementDirection;
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
