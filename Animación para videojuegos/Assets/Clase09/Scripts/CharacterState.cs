using UnityEngine;
using UnityEngine.UI;

public interface IPlayerStats {
    float Speed { get; }
    float MaxHealth { get; }
    float Damage { get; }
    float CurrentStamine { get; }
}

public class CharacterState : MonoBehaviour, IPlayerStats
{
    [Header("Stamina")]
    [SerializeField] public float _startStamina = 1000;
    [SerializeField] public float _staminaRegen = 1;
    [SerializeField] public float _currentStamina = 1000;

    [Header("Health")]
    [SerializeField] public float _startHealth = 100;
    [SerializeField] public float _currentHealth = 100;

    [Header("Speed")]
    [SerializeField] public float _baseSpeed = 5f;

    [Header("Damage")]
    [SerializeField] public float _baseDamage = 10f;
    public float CurrentStamine => _currentStamina;
    public float Speed => _baseSpeed;       
    public float MaxHealth => _startHealth;
    public float Damage => _baseDamage;

    [SerializeField] Slider slider;
    private void RegenerateStamina(float regenAmount) 
    {
        _currentStamina = Mathf.Min(_currentStamina + regenAmount, _startStamina);
    }
    private float GetStaminaDepletion() 
    {
        return 10;
    }
    public void DepletStamina(float staminaDepletion) 
    {
        _currentStamina = GetStaminaDepletion() * staminaDepletion;
    }

    public void DepleteHealth(float healthDepletion, out bool zeroHealth) 
    {
        slider.value = healthDepletion;
        _currentHealth -= healthDepletion;
        zeroHealth = false;
        if (_currentHealth <= 0) 
        {
            zeroHealth = true;
        }
    }

    private void Start()
    {
        _currentStamina = _startStamina;
        _currentHealth = _startHealth;
        slider.value = _currentHealth;
    }
    private void Update()
    {
        RegenerateStamina(_staminaRegen * Time.deltaTime);

    }
}