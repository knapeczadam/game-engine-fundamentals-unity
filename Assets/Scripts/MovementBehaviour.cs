using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 1.0f;
    
    [SerializeField]
    private float _rotationSpeed = 10.0f;
    
    private Rigidbody _rigidbody;
    
    private Vector3 _desiredMovementDirection = Vector3.zero;
    public Vector3 DesiredMovementDirection 
    { 
        get => _desiredMovementDirection; 
        set => _desiredMovementDirection = value; 
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }
    
    private void HandleMovement()
    {
        if (_rigidbody is null)
        {
            Debug.LogError("Rigidbody is not set in the MovementBehaviour component.");
            return;
        }
        
        Vector3 movement = _desiredMovementDirection.normalized;
        movement *= _movementSpeed * Time.deltaTime;
        // transform.position += movement;
        _rigidbody.MovePosition(transform.position + movement);
    }
    
    private void HandleRotation()
    {
        if (_desiredMovementDirection != Vector3.zero)
        {
            // Rotates the character to face the direction of movement
            // transform.rotation = Quaternion.LookRotation(_desiredMovementDirection);
            
            // Smoothly rotates the character to face the direction of movement.
            Quaternion targetRotation = Quaternion.LookRotation(_desiredMovementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
