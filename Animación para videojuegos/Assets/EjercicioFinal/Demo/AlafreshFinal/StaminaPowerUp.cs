using UnityEngine;

public class StaminaPowerUp : PlayerStatsDecorator
{
    private readonly float extraStamina;

    public StaminaPowerUp(IPlayerStats inner, float extraStamina)
        : base(inner) {
        this.extraStamina = extraStamina;
    }

    public override float CurrentStamine => base.CurrentStamine + extraStamina;
}
