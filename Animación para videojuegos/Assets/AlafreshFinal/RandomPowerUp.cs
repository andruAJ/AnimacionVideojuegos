using UnityEngine;

public class RandomPowerUp : PlayerStatsDecorator
{
    private readonly IPlayerStats chosen;

    public RandomPowerUp(IPlayerStats inner)
        : base(inner) {
        int r = Random.Range(0, 4); // 0,1,2,3

        switch (r) {
            case 0:
                chosen = new SpeedPowerUp(inner, 1.5f);
                break;
            case 1:
                chosen = new HealthPowerUp(inner, 25f);
                break;
            case 2:
                chosen = new DamagePowerUp(inner, 10f);
                break;
            case 3:
            default:
                chosen = new StaminaPowerUp(inner, 200f);
                break;
        }
    }

    public override float Speed => chosen.Speed;
    public override float MaxHealth => chosen.MaxHealth;
    public override float Damage => chosen.Damage;
    public override float CurrentStamine => chosen.CurrentStamine;
}
