using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyDamageHitBox : MonoBehaviour
{
    private EnemyDamageController damageController;

    private void Awake()
    {
        damageController = GetComponentInParent<EnemyDamageController>();
    }

    // Este método será llamado por el AttackHitBox del jugador
    public void ReceiveDamage(DamageMessage message)
    {
        if (damageController != null)
        {
            Debug.Log($"EnemyDamageHitBox received damage: {message.amount} of level {message.damageLevel}");
            damageController.EnqueueDamage(message);
        }
    }
}