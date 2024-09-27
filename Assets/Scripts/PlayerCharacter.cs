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
        
        if (_inputAsset == null)
        {
            Debug.LogError("Input Asset is not set in the PlayerCharacter component.");
            return;
        }
    }

    private void OnEnable()
    {
        if (_inputAsset)
        {
            _inputAsset.Enable();
        }
    }
    
    private void OnDisable()
    {
        if (_inputAsset)
        {
            _inputAsset.Disable();
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
    
    private void HandleRescue()
    {
    }
}
