using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    public PowerUpType type;

    private void OnTriggerEnter(Collider other) {
        var powerUpManager = other.GetComponent<PlayerPowerUpManager>();
        if (powerUpManager == null) return;

        switch (type) {
            case PowerUpType.Speed:
                powerUpManager.ApplySpeedPowerUp(1.5f);
                break;
            case PowerUpType.Health:
                powerUpManager.ApplyHealthPowerUp(25f);
                break;
            case PowerUpType.Damage:
                powerUpManager.ApplyDamagePowerUp(10f);
                break;
            case PowerUpType.RandomOne:
                powerUpManager.ApplyRandomPowerUp();
                break;
        }

        Destroy(gameObject);
    }
}
