using UnityEngine;

public class DamagePowerUp : PlayerStatsDecorator
{
    private readonly float extraDamage;

    public DamagePowerUp(IPlayerStats inner, float extraDamage)
        : base(inner) {
        this.extraDamage = extraDamage;
    }

    public override float Damage => base.Damage + extraDamage;
}
