using System;
using UnityEngine;
using UnityEngine.Events;

public class DamageHitBox : MonoBehaviour, IdamageReceiver<DamageMessage>
{
    [Serializable]
    public class AttackQueueEvent : UnityEvent<DamageMessage>
    {
        
    }
    [SerializeField] private float defenseMultiplier;
    public AttackQueueEvent onHit;
    public void ReceiveDamage (DamageMessage damage)
    {
        if (damage.sender == transform.root.gameObject) return;
        damage.amount *= defenseMultiplier;
        onHit?.Invoke(damage);
    }
}
