using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

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
    [SerializeField] private GameObject boss;

    [SerializeField] private WaveData[] waves;

    [SerializeField] private AudioClip endWave;
    [SerializeField] private AudioClip bossLaugh;
    [SerializeField] private GameObject bossHealthUI;
    [SerializeField] private GameObject healthPickup;

    private AudioSource _audioSource;

    
   private void Start()
    {
        _currentWave = 0;
        _audioSource = GetComponent<AudioSource>();
        StartWave();
    }

   
    private void Update()
    {
        if (_currentWave >= waves.Length) return; // If we've reached the boss wave, stop spawning regular enemies
        _spawnTimer += Time.deltaTime; // Increment spawn timer

        if (_enemiesAlive < waves[_currentWave].maxSimultaneous && _currentEnemiesSpawned < waves[_currentWave].totalEnemies && _spawnTimer >= waves[_currentWave].spawnInterval) // Check if we can spawn a new enemy based on alive count, total count for the wave, and spawn interval
        {
            SpawnEnemy();
            _spawnTimer = 0;
        }
    }

    private void StartWave() // Method when a new wave starts resets counters and timers
    {
        _spawnTimer = 0;
        _enemiesAlive = 0;
        _currentEnemiesSpawned = 0;

        waveText.text = $"Wave {_currentWave + 1}"; // Update wave text to show current wave number

        if (_currentWave > 0)
        {
            _audioSource.PlayOneShot(endWave); // Play end wave sound when starting a new wave, but not on the first wave
        }

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

        if (_enemiesAlive == 0 && _currentEnemiesSpawned >= waves[_currentWave].totalEnemies) // Check if all enemies for the current wave have been spawned and are dead
        {
            _currentWave++;

            if (_currentWave >= waves.Length) // Boss wave
            {
                waveText.text = $"Wave {_currentWave + 1}";

                int randomIndex = Random.Range(0, spawnPoints.Length);
                boss.transform.position = spawnPoints[randomIndex].position;
                healthPickup.SetActive(true); // Activate the health pickup when the boss wave starts
                boss.SetActive(true); // Activate the boss when the boss wave starts
                boss.GetComponent<AudioSource>().PlayOneShot(bossLaugh); // Play boss laugh sound when the boss spawns
                bossHealthUI.SetActive(true); // Show the boss health UI when the boss spawns
            }
            else
            {
                StartWave();
            }

            
        }
    }

    public void BossDied()
    {
        
    }
}
