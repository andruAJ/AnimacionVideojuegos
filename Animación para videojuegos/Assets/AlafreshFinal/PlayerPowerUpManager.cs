using UnityEngine;

public enum PowerUpType {
    Speed,
    Health,
    Damage,
    Stamina,
    RandomOne
}
public class PlayerPowerUpManager : MonoBehaviour
{
    public IPlayerStats CurrentStats { get; private set; }

    private IPlayerStats _baseStats;
    private CharacterState _characterState;

    private void Awake() {
        _characterState = GetComponent<CharacterState>();
        _baseStats = _characterState;
        CurrentStats = _baseStats;
    }

    public void ApplySpeedPowerUp(float multiplier) {
        CurrentStats = new SpeedPowerUp(CurrentStats, multiplier);
        SyncWithCharacterState();
    }

    public void ApplyHealthPowerUp(float extra) {
        CurrentStats = new HealthPowerUp(CurrentStats, extra);
        SyncWithCharacterState();
    }

    public void ApplyDamagePowerUp(float extra) {
        CurrentStats = new DamagePowerUp(CurrentStats, extra);
        SyncWithCharacterState();
    }

    public void ApplyRandomPowerUp() {
        CurrentStats = new RandomPowerUp(CurrentStats);
        SyncWithCharacterState();
    }

    public void ApplyStaminaPowerUp() {
        CurrentStats = new StaminaPowerUp(CurrentStats, 200f);
        SyncWithCharacterState();
    }

    private void SyncWithCharacterState() {
        // 1. Velocidad y daño (lo que quieres ver en el inspector)
        _characterState._baseSpeed = CurrentStats.Speed;
        _characterState._baseDamage = CurrentStats.Damage;

        // 2. Vida máxima y vida actual
        _characterState._startHealth = CurrentStats.MaxHealth;
        if (_characterState._currentHealth > _characterState._startHealth)
            _characterState._currentHealth = _characterState._startHealth;

        // 3. Stamina (tu interfaz solo tiene CurrentStamine, así que copiamos eso)
        _characterState._startStamina = CurrentStats.CurrentStamine;

        // Si quieres que el “máximo” de stamina también suba:
        // _characterState._startStamina = CurrentStats.CurrentStamine;
    }

}
