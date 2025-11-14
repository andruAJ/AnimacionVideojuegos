using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }

    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameObject gameOverUI;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void GameOver() {
        if (IsGameOver) return;

        IsGameOver = true;
        waveManager.SetGameOver();

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOver);

        Time.timeScale = 0f;
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }
}
