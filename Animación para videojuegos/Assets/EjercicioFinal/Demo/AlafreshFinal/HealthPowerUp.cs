using UnityEngine;

public class HealthPowerUp : PlayerStatsDecorator
{
    private readonly float extraHealth;

    public HealthPowerUp(IPlayerStats inner, float extraHealth)
        : base(inner) {
        this.extraHealth = extraHealth;
    }

    public override float MaxHealth => base.MaxHealth + extraHealth;
}
