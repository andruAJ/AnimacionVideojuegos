using UnityEngine;
using System;
using UnityEngine.UI;

public class AttackHitBox : MonoBehaviour, IdamageSender<DamageMessage>
{
    [SerializeField] private DamageMessage damageMessage;
    [SerializeField] Slider playerSlider;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out IdamageReceiver<DamageMessage> receiver)) 
        {
            SendDamage(receiver);
        }
    }
    public void SendDamage(IdamageReceiver<DamageMessage> receiver) 
    {
        playerSlider.value -= damageMessage.amount;
        receiver.ReceiveDamage(damageMessage);
    }
}
