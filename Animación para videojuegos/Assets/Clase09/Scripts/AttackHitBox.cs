using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private DamageMessage.DamageLevel damageLevel = DamageMessage.DamageLevel.Small;
    [SerializeField] private GameObject sender;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[AHB]AttackHitBox triggered by {other.gameObject.name}");
        var damageHitBox = other.GetComponent<EnemyDamageHitBox>();
        if (damageHitBox != null)
        {
            DamageMessage message = new DamageMessage
            {
                sender = sender != null ? sender : gameObject,
                amount = damageAmount,
                damageLevel = damageLevel
            };
            damageHitBox.ReceiveDamage(message);
        }
    }
}
