using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private float _startStamina = 1000;
    [SerializeField] private float _staminaRegen = 1;
    [SerializeField] private float _currentStamina = 1000;
    [SerializeField] private float _startHealth = 100;
    [SerializeField] private float _currentHealth = 100;

    public float CurrentStamine => _currentStamina;

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
    }
    private void Update()
    {
        RegenerateStamina(_staminaRegen * Time.deltaTime);

    }
}