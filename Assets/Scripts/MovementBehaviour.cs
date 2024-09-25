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

    protected GameObject _target;
    public GameObject Target
    {
        get => _target;
        set => _target = value;
    }

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
    
    protected virtual void HandleRotation()
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
