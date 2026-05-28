using UnityEngine;
using TMPro;

[System.Serializable]
public struct WaveData // Struct to hold data for each wave, can be edited in the inspector
{
    public int totalEnemies;
    public float spawnInterval;
    public int maxSimultaneous;
}
public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    private void Awake()
    {
        instance = this; // Singleton to allow other scripts to access the wave manager if needed
    }

    private int _currentEnemiesSpawned; // Counter for how many enemies have been spawned at the moment in the current wave
    private int _enemiesAlive; // Counter for how many enemies are currently alive, used to limit simultaneous spawns
    private int _currentWave;
    private float _spawnTimer; // Timer to track time between spawns, used to control spawn intervals

    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private TMP_Text waveText;

    [SerializeField] private WaveData[] waves;
    
   private void Start()
    {
        _currentWave = 0;
        StartWave();
    }

   
    private void Update()
    {
        _spawnTimer += Time.deltaTime; // Increment spawn timer

        if (_enemiesAlive < waves[_currentWave].maxSimultaneous && _currentEnemiesSpawned < waves[_currentWave].totalEnemies && _spawnTimer >= waves[_currentWave].spawnInterval)
        {
            SpawnEnemy();
            _spawnTimer = 0;
        }
    }

    private void StartWave() // Method to start a new wave, resets counters and timers
    {
        _spawnTimer = 0;
        _enemiesAlive = 0;
        _currentEnemiesSpawned = 0;

        waveText.text = $"Wave {_currentWave + 1}";
    }

    private void SpawnEnemy() // Method to spawn an enemy at a random spawn point, increments counters for spawned and alive enemies
    {
        _currentEnemiesSpawned++;
        _enemiesAlive++;

        int randomNext = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[randomNext].position, Quaternion.identity);
    }

    public void EnemyDied() // Method to be called when an enemy dies, decrements alive counter and checks if the wave is complete to start the next one
    {
        _enemiesAlive--;

        if (_enemiesAlive == 0 && _currentEnemiesSpawned >= waves[_currentWave].totalEnemies)
        {
            _currentWave++;

            if (_currentWave >= waves.Length)
            {
                //boss
            }
            else
            {
                StartWave();
            }

            
        }
    }
}
