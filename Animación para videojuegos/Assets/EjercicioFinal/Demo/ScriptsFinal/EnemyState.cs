using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startHealth = 100;
    [SerializeField] private float currentHealth = 100;

    [Header("Movement")]
    [SerializeField] private float speed = 100;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => startHealth;

    public void DepleteHealth(float amount, out bool isDead)
    {
        currentHealth -= amount;
        isDead = currentHealth <= 0;
    }

    private void Start()
    {
        currentHealth = startHealth;
    }
}
