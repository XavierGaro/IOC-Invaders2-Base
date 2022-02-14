using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Si la dejamos privada esto no debe verse!
    private List<WaveConfigSO> _waveConfigs;
    private float _timeBetweenWaves = 1f;
    private int _loops = 1;

    private WaveConfigSO _currentWave;
    private int _spawnedEnemies = 0;
    private bool _spawning = false;


    public delegate void OnEventDelegate();

    public event OnEventDelegate OnAllEnemiesDestroyed;

    public void SetWaveConfigs(List<WaveConfigSO> waveConfigs, int loops, float timeBetweenWaves)
    {
        _loops = loops;
        _waveConfigs = waveConfigs;
        _timeBetweenWaves = timeBetweenWaves;

    }


    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemyWaves());
    }


    IEnumerator SpawnEnemyWaves()
    {
        _spawning = true;
        for (int i = 0; i < _loops; i++)
        {
            foreach (WaveConfigSO wave in _waveConfigs)
            {
                _currentWave = wave;

                for (int j = 0; j < _currentWave.GetEnemyCount(); j++)
                {
                    SpawnEnemy(_currentWave.GetEnemyPrefab(j));
                    yield return new WaitForSeconds(_currentWave.GetSpawnTime());
                }

                yield return new WaitForSeconds(_timeBetweenWaves);
            }
        }

        _spawning = false;
        TryToEndLevel();
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab,
            _currentWave.GetStartingWaypoint().position,
            Quaternion.Euler(0f, 0f, 180f),
            transform); // Añadimos el spawner como parent

        _spawnedEnemies++;

        enemy.GetComponent<Health>().OnDestroyEvent += OnEnemyDestroyed;
    }


    void OnEnemyDestroyed()
    {
        _spawnedEnemies--;
        TryToEndLevel();
    }

    // Es necesari aquesta funció perque hi ha un delay entre que apareix el darrer enemic i finalitza
    // el loop, si en aquest moment s'elimina al darrer enemic llavors el joc es quedaria bloquejat
    // si no controlem en els dos moments: quan finalitza l'spawn i quan s'elimina al darrer enemic
    // actiu
    void TryToEndLevel()
    {
        if (_spawnedEnemies == 0 && !_spawning && OnAllEnemiesDestroyed != null)
        {
            OnAllEnemiesDestroyed();
        }
    }

    public WaveConfigSO GetCurrentWave()
    {
        return _currentWave;
    }
}