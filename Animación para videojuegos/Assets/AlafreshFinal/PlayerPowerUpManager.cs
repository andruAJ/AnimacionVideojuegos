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

    private void Awake() {
        // CharacterState ya implementa IPlayerStats
        _baseStats = GetComponent<CharacterState>();
        CurrentStats = _baseStats;
    }

    public void ApplySpeedPowerUp(float multiplier) {
        CurrentStats = new SpeedPowerUp(CurrentStats, multiplier);
    }

    public void ApplyHealthPowerUp(float extra) {
        CurrentStats = new HealthPowerUp(CurrentStats, extra);
    }

    public void ApplyDamagePowerUp(float extra) {
        CurrentStats = new DamagePowerUp(CurrentStats, extra);
    }

    public void ApplyRandomPowerUp() {
        CurrentStats = new RandomPowerUp(CurrentStats);
    }

    public void ApplyStaminaPowerUp() {
        CurrentStats = new StaminaPowerUp(CurrentStats, 200f);
    }
}
