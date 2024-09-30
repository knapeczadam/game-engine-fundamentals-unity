using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyCharacter : BasicCharacter
{
    private GameObject _playerTarget = null;
    
    [SerializeField]
    private float _attackRange = 2.0f;

    private void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player)
        {
            _playerTarget = player.gameObject;
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        if (_movementBehaviour)
        {
            _movementBehaviour.Target = _playerTarget;
        }
    }

    private void HandleAttack()
    {
        if (_attackBehaviour && _playerTarget)
        {
            if ((transform.position - _playerTarget.transform.position).sqrMagnitude <= _attackRange * _attackRange)
            {
                Debug.Log("Attacking player");
                _attackBehaviour.Attack();
                // Invoke(nameof(Die), 0.2f);
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
    
}
