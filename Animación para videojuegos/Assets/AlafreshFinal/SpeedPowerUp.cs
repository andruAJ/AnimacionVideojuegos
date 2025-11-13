using UnityEngine;

public class SpeedPowerUp : PlayerStatsDecorator {
    
    private readonly float multiplier;
    public SpeedPowerUp(IPlayerStats inner, float multiplier)
        : base(inner) {
        multiplier = Mathf.Max(0f, multiplier);
        this.multiplier = multiplier;
    }
    public override float Speed => base.Speed * multiplier;
}
