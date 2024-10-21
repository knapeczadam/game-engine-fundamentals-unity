using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : Health
{
    [SerializeField] private List<GameObject> _bloodEffects = new List<GameObject>();
    private float _deathTime = 10.0f;

    private void Start()
    {
        _deathTime = UnityEngine.Random.Range(10.0f, 20.0f);
    }

    public override bool TakeDamage(float damage)
    {
        if (!base.TakeDamage(damage)) return false;
        
        if (_bloodEffects.Count > 0)
        {
            CreateBloodEffectInstance();
        }
        
        // first disable the agent
        var agent = GetComponent<NavMeshAgent>();
        var movement = GetComponent<NavMeshMovementBehaviour>();
        movement.enabled = false;
        
        agent.isStopped = true;
        // agent.enabled = false;
        
        // add rigidbody temporarily to allow the enemy to get pushed back
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(-transform.forward, ForceMode.Impulse);
        
        // disable the rigidbody after 0.5 seconds
        StartCoroutine(DisableRigidbody(rb, agent, movement));
        
        return true;
    }

    private IEnumerator DisableRigidbody(Rigidbody rb, NavMeshAgent agent, NavMeshMovementBehaviour movement)
    {
        yield return new WaitForSeconds(0.5f);
        if (IsDead) yield break;
        
        rb.isKinematic = true;
        // agent.enabled = true;
        movement.enabled = true;
    }

    private void CreateBloodEffectInstance()
    {
        var bloodEffect = _bloodEffects[UnityEngine.Random.Range(0, _bloodEffects.Count)];
        var yOffset = UnityEngine.Random.Range(0.001f, 0.10f);
        var position = new Vector3(transform.position.x, yOffset, transform.position.z);
        var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
        Instantiate(bloodEffect, position, rotation);
    }

    protected override void Die()
    {
        // Common components to disable when an enemy dies
        GetComponent<Health>().enabled                   =        false;
        GetComponent<NavMeshAgent>().enabled             =        false;
        GetComponent<NavMeshMovementBehaviour>().enabled =        false;
        GetComponent<BasicEnemyCharacter>().enabled      =        false;
        
        var renderer = GetComponentInChildren<Renderer>();
        renderer.material.color = Color.black;
        
        var attack = GetComponent<AttackBehaviour>();
        if (attack)
        {
            attack.enabled = false;
        }

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.mass = 0.01f;
        Destroy(gameObject, _deathTime);
    }
}
