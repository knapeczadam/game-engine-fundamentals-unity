using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 1.0f;
    
    private Vector3 _desiredMovementDirection = Vector3.zero;
    public Vector3 DesiredMovementDirection 
    { 
        get => _desiredMovementDirection; 
        set => _desiredMovementDirection = value; 
    }

    private void Update()
    {
        HandleMovement();
    }
    
    private void HandleMovement()
    {
        Vector3 movement = _desiredMovementDirection.normalized;
        movement *= _movementSpeed * Time.deltaTime;
        transform.position += movement;
    }
}
