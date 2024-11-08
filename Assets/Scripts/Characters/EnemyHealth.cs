using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyHealth : Health
{
    [SerializeField] private List<GameObject> m_bloodEffects = new List<GameObject>();
    [SerializeField] private GameObject          m_bloodSplatter    = null;
    [SerializeField] private SkinnedMeshRenderer m_bodyRendererLOD0 = null;
    [SerializeField] private SkinnedMeshRenderer m_bodyRendererLOD1 = null;
    [SerializeField] private SkinnedMeshRenderer m_bodyRendererLOD2 = null;
    private float m_deathTime = 10.0f;
    
    private int m_id        = 0;
    private int m_hitScore  = 0;
    private int m_killScore = 0;

    protected override void Awake()
    {
        base.Awake();
        m_id = GetComponent<MyID>().GetID();
        m_hitScore = GetComponent<Score>().HitScore;
        m_killScore = GetComponent<Score>().KillScore;
    }

    private void Start()
    {
        m_deathTime = UnityEngine.Random.Range(10.0f, 20.0f);
    }

    public override bool TakeDamage(float damage, float forceMultiplier)
    {
        // if the enemy is already dead, return false
        if (!base.TakeDamage(damage)) return false;
        
        ScoreManager.Instance.AddScore(m_hitScore);
        
        // if the enemy is still alive, create a blood effect
        if (m_bloodEffects.Count > 0)
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
        if (m_isDead) yield break;
        
        rb.isKinematic = true;
        // agent.enabled = true;
        movement.enabled = true;
    }

    private void CreateBloodEffectInstance()
    {
        var bloodEffect = m_bloodEffects[UnityEngine.Random.Range(0, m_bloodEffects.Count)];
        var yOffset = UnityEngine.Random.Range(0.001f, 0.10f);
        var position = new Vector3(transform.position.x, yOffset, transform.position.z);
        var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
        Instantiate(bloodEffect, position, rotation);
        Instantiate(m_bloodSplatter, new Vector3(transform.position.x, 1.4f, transform.position.z), rotation);
    }

    protected override void Die()
    {
        // Common components to disable when an enemy dies
        GetComponent<Health>().enabled                   =        false;
        GetComponent<NavMeshAgent>().enabled             =        false;
        GetComponent<NavMeshMovementBehaviour>().enabled =        false;
        GetComponent<BasicEnemyCharacter>().enabled      =        false;
        
        m_bodyRendererLOD0.material.color = Color.black;
        m_bodyRendererLOD0.material.DisableKeyword("_EMISSION");
        m_bodyRendererLOD0.material.SetFloat("_EmissiveExposureWeight", 0.0f);
        
        m_bodyRendererLOD1.material.color = Color.black;
        m_bodyRendererLOD1.material.DisableKeyword("_EMISSION");
        m_bodyRendererLOD1.material.SetFloat("_EmissiveExposureWeight", 0.0f);
        
        m_bodyRendererLOD2.material.color = Color.black;
        m_bodyRendererLOD2.material.DisableKeyword("_EMISSION");
        m_bodyRendererLOD2.material.SetFloat("_EmissiveExposureWeight", 0.0f);
        
        var attack = GetComponent<AttackBehaviour>();
        if (attack)
        {
            attack.enabled = false;
        }

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.mass = 0.01f;
        // Destroy(gameObject, m_deathTime);
        
        // despawn the enemy
        SpawnManager.Instance.Despawn(m_id);
        
        // add the kill score to the player
        ScoreManager.Instance.AddScore(m_killScore);
    }
}
