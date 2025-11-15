using UnityEngine;

public enum EnemyType {
    Melee,
    Ranged
}
public class SimpleEnemy : MonoBehaviour
{
    [Header("Stats base")]
    [SerializeField] private float baseSpeed = 2f;
    [SerializeField] private float baseDamage = 10f;

    private float currentSpeed;
    private float currentDamage;

    private Transform target;
    private WaveManager owner;
    private EnemyType type;

    // Llamado por el WaveManager cuando se saca del pool
    public void Init(WaveManager owner, EnemyType type, Transform target,
                     float speedMultiplier, float damageMultiplier) {
        this.owner = owner;
        this.type = type;
        this.target = target;

        currentSpeed = baseSpeed * speedMultiplier;
        currentDamage = baseDamage * damageMultiplier;

        gameObject.SetActive(true);
    }

    private void Update() {
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * currentSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.collider.CompareTag("Player")) return;

        var state = collision.collider.GetComponent<CharacterState>();
        if (state != null) {
            bool isDead;
            state.DepleteHealth(currentDamage, out isDead);

            if (isDead) {
                GameManager.Instance.GameOver();
            }
        }

        Die();
    }

    public void Die() {
        // En lugar de Destroy, lo devolvemos al pool
        owner.ReturnToPool(gameObject, type);
    }
}
