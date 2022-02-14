using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Config", fileName = "New LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    [SerializeField] public string sceneName = "Game";
    [SerializeField] public string levelTitle = "Level";
    [SerializeField] public AudioClip music = null;
    [SerializeField] public float timeBetweenWaves = 1f;
    [SerializeField] public int loops = 3;
    [SerializeField] public List<WaveConfigSO> waveConfigs;
}