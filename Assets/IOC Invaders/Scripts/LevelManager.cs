using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
// Aquesta classe no ha de ser un Singleton, perquè necesistem saber quan comença el nivell
public class LevelManager : MonoBehaviour
{
    public delegate void OnEventDelegate();
    public event OnEventDelegate OnLevelEnd;
    
    private EnemySpawner _enemySpawner;
    private LevelConfigSO _currentLevelConfig;


    private void Awake()
    {
        _currentLevelConfig = GetConfigLevel(GameState.Instance.CurrentLevel);
    }

    private void Start()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _enemySpawner.OnAllEnemiesDestroyed += EndLevel;

        
        
        _enemySpawner.SetWaveConfigs(
            _currentLevelConfig.waveConfigs,
            _currentLevelConfig.loops,
            _currentLevelConfig.timeBetweenWaves);
        
        AudioManager.Instance.PlayTrack(_currentLevelConfig.music);
        StartCoroutine(StartLevel());
        
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(GameManager.GameLevels.TimeBeforeStartLevel);
        _enemySpawner.StartSpawn();
    }


    public LevelConfigSO GetConfigLevel(int level)
    {
        var gl = GameManager.GameLevels; 
        return GameManager.GameLevels.Levels[level % GameManager.GameLevels.Levels.Count];
    }

    public String GetCurrentLevelTitle()
    {
        return _currentLevelConfig.levelTitle;
    }

    private void EndLevel()
    {
        GameState.Instance.CurrentLevel++;

        if (OnLevelEnd != null) OnLevelEnd();

        GameManager.LoadLevel(GetConfigLevel(GameState.Instance.CurrentLevel).sceneName);
    }
}