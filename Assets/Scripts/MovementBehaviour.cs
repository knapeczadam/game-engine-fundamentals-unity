using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed = 1.0f;
    
    [SerializeField]
    private float _rotationSpeed = 10.0f;
    
    protected Rigidbody _rigidbody;
    
    protected Vector3 _desiredMovementDirection = Vector3.zero;
    public Vector3 DesiredMovementDirection 
    { 
        get => _desiredMovementDirection; 
        set => _desiredMovementDirection = value; 
    }
    
    protected bool _isRunning = false;
    public bool IsRunning
    {
        get => _isRunning;
        set => _isRunning = value;
    }

    protected GameObject _target;
    public GameObject Target
    {
        get => _target;
        set => _target = value;
    }
    
    [SerializeField]
    private bool _smoothRotation = true;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }
    
    protected virtual void HandleMovement()
    {
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody is not set in the MovementBehaviour component.");
            return;
        }
        
        Vector3 movement = _desiredMovementDirection.normalized;
        float movementSpeed = _movementSpeed;
        if (_isRunning)
        {
            movementSpeed *= 2.0f;
        }
        movement *= movementSpeed * Time.deltaTime;
        // transform.position += movement;
        _rigidbody.MovePosition(transform.position + movement);
    }
    
    protected virtual void HandleRotation()
    {
        if (_desiredMovementDirection != Vector3.zero)
        {
            if (_smoothRotation)
            {
                // Smoothly rotates the character to face the direction of movement.
                Quaternion targetRotation = Quaternion.LookRotation(_desiredMovementDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
            else
            {
                // Rotates the character to face the direction of movement
                transform.rotation = Quaternion.LookRotation(_desiredMovementDirection);
            }
            
        }
    }
}
