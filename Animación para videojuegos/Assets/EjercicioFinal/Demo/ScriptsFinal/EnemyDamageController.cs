using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    private List<DamageMessage> damageList = new List<DamageMessage>();

    [SerializeField] private bool ignoreDamage;

    private Animator animator;

    private EnemyState enemyState;

    [Header("Destruction")]
    [SerializeField] private float destroyDelay = 5f;
    private bool deathHandled = false;

    public void EnqueueDamage(DamageMessage damage)
    {
        if (ignoreDamage || damageList.Any(dmg => dmg.sender == damage.sender)) return;
        Debug.Log("Damage Enqueued");
        damageList.Add(damage);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();
    }
    public void IFrameStart()
    {
        ignoreDamage = true;
    }
    public void IFrameEnd()
    {
        ignoreDamage = false;
    }
    private void Update()
    {
        int damageLevel = 0;
        bool isDead = false;

        foreach (DamageMessage message in damageList)
        {
            enemyState.DepleteHealth(message.amount, out isDead);
            Debug.Log($"Enemy Health: {enemyState.CurrentHealth}/{enemyState.MaxHealth}");
            damageLevel = Mathf.Max(damageLevel, (int)message.damageLevel);
        }
        if (damageList.Count == 0) return;
        animator.SetTrigger("Damage");

        if (isDead && !deathHandled)
        {
            HandleDeath();
        }
        damageList.Clear();
    }
    private void HandleDeath()
    {
        deathHandled = true;   
        animator.SetTrigger("Death");

        var behaviours = GetComponents<Behaviour>();
        foreach (var b in behaviours)
        {
            if (b == this) continue;         
            if (b == animator) continue;     
            b.enabled = false;
        }

        var coll = GetComponent<Collider>();
        if (coll != null) coll.enabled = false;

        var rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
