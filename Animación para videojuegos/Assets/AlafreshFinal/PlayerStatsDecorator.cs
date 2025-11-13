using UnityEngine;

public class PlayerStatsDecorator : IPlayerStats {
    protected IPlayerStats inner;

    protected PlayerStatsDecorator(IPlayerStats inner) {
        this.inner = inner;
    }

    public virtual float Speed => inner.Speed;
    public virtual float MaxHealth => inner.MaxHealth;
    public virtual float Damage => inner.Damage;
    public virtual float CurrentStamine => inner.CurrentStamine;

}
