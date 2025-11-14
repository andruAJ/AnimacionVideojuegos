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
        Debug.Log("[DHB]Damage.sender: "+ damage.sender + " transform: " + transform.root.gameObject);
        if (damage.sender == transform.root.gameObject) return;
        Debug.Log($"[DHB]Damage HitBox received damage: {damage.amount} from {damage.sender.name}");
        damage.amount *= defenseMultiplier;
        onHit?.Invoke(damage);
    }
}
