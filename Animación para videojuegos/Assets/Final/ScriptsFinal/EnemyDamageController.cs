using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyDamageController : MonoBehaviour
{
    private List<DamageMessage> damageList = new List<DamageMessage>();

    [SerializeField] private bool ignoreDamage;

    private Animator animator;

    private EnemyState enemyState;

    public void EnqueueDamage(DamageMessage damage)
    {
        if (ignoreDamage || damageList.Any(dmg => dmg.sender == damage.sender)) return;
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
            Game.Instance.PlayerOne.DepleteHealth(message.amount, out isDead);
            damageLevel = Mathf.Max(damageLevel, (int)message.damageLevel);
        }
        if (damageList.Count == 0) return;
        animator.SetTrigger("Damage");

        if (isDead)
        {
            animator.SetTrigger("Death");
        }
        damageList.Clear();
    }
}
