using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 1.0f;

    private Vector3 _desiredHorizontalMovementDirection = Vector3.zero;
    public Vector3 DesiredHorizontalMovementDirection 
    { 
        get => _desiredHorizontalMovementDirection; 
        set => _desiredHorizontalMovementDirection = value; 
    }
    
    private Vector3 _desiredVerticalMovementDirection = Vector3.zero;
    public Vector3 DesiredVerticalMovementDirection 
    { 
        get => _desiredVerticalMovementDirection; 
        set => _desiredVerticalMovementDirection = value; 
    }

    private void Update()
    {
        HandleHorizontalMovement();
        HandleVerticalMovement();
    }
    
    private void HandleHorizontalMovement()
    {
        Vector3 horizontalMovement = _desiredHorizontalMovementDirection.normalized;
        horizontalMovement *= _movementSpeed * Time.deltaTime;
        transform.position += horizontalMovement;
    }
    
    private void HandleVerticalMovement()
    {
        Vector3 verticalMovement = _desiredVerticalMovementDirection.normalized;
        verticalMovement *= _movementSpeed * Time.deltaTime;
        transform.position += verticalMovement;
    }
}
