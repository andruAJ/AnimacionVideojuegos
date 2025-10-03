using UnityEngine;
using System;

public class AttackHitBox : MonoBehaviour, IdamageSender<DamageMessage>
{
    [SerializeField] private DamageMessage damageMessage;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out IdamageReceiver<DamageMessage> receiver)) 
        {
            SendDamage(receiver);
        }
    }
    public void SendDamage(IdamageReceiver<DamageMessage> receiver) 
    {
        receiver.ReceiveDamage(damageMessage);
    }
}
