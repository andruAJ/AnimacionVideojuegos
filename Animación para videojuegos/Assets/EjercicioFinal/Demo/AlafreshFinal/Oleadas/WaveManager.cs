using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Prefabs de enemigos")]
    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private GameObject rangedPrefab;

    [Header("Spawns")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform playerTarget; // el transform del jugador

    [Header("Oleadas")]
    [SerializeField] private int baseEnemiesPerWave = 3;
    [SerializeField] private float timeBetweenWaves = 3f;

    [Header("Escalado de dificultad")]
    [SerializeField] private float speedStepPerWave = 0.1f;
    [SerializeField] private float damageStepPerWave = 0.1f;

    private List<GameObject> meleePool = new List<GameObject>();
    private List<GameObject> rangedPool = new List<GameObject>();

    private bool gameOver;

    private void Start() {
        StartCoroutine(WaveLoop());
    }

    public void SetGameOver() {
        gameOver = true;
    }

    private IEnumerator WaveLoop() {
        int waveIndex = 0;

        while (!gameOver) {
            int enemyCount = baseEnemiesPerWave + waveIndex;

            float speedMultiplier = 1f + waveIndex * speedStepPerWave;
            float damageMultiplier = 1f + waveIndex * damageStepPerWave;

            Debug.Log($"Wave {waveIndex} - Enemies: {enemyCount}, Speed x{speedMultiplier}, Damage x{damageMultiplier}");

            for (int i = 0; i < enemyCount; i++) {
                SpawnEnemy(speedMultiplier, damageMultiplier);
                yield return new WaitForSeconds(0.3f);
            }

            waveIndex++;
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private void SpawnEnemy(float speedMultiplier, float damageMultiplier) {
        bool spawnMelee = Random.value < 0.5f;
        GameObject prefab = spawnMelee ? meleePrefab : rangedPrefab;
        List<GameObject> pool = spawnMelee ? meleePool : rangedPool;
        EnemyType type = spawnMelee ? EnemyType.Melee : EnemyType.Ranged;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemyObj = GetFromPool(prefab, pool);
        enemyObj.transform.position = spawnPoint.position;
        enemyObj.transform.rotation = spawnPoint.rotation;

        var simpleEnemy = enemyObj.GetComponent<SimpleEnemy>();
        if (simpleEnemy != null) {
            simpleEnemy.Init(this, type, playerTarget, speedMultiplier, damageMultiplier);
        }
    }

    private GameObject GetFromPool(GameObject prefab, List<GameObject> pool) {
        GameObject obj;

        if (pool.Count > 0) {
            obj = pool[0];
            pool.RemoveAt(0);
            obj.SetActive(true);
        } else {
            obj = Instantiate(prefab);
        }

        return obj;
    }

    public void ReturnToPool(GameObject enemy, EnemyType type) {
        enemy.SetActive(false);

        if (type == EnemyType.Melee) {
            meleePool.Add(enemy);
        } else {
            rangedPool.Add(enemy);
        }
    }
}
