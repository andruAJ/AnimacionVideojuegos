using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    [SerializeField] GameObject gameObjectUI;
    public PowerUpType type;

    private void OnTriggerEnter(Collider other) {
        var powerUpManager = other.GetComponent<PlayerPowerUpManager>();
        if (powerUpManager == null) return;
        gameObjectUI.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.collectPowrUp);
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
            case PowerUpType.Stamina:
                powerUpManager.ApplyStaminaPowerUp();
                break;
        }

        Destroy(gameObject);
    }
}
