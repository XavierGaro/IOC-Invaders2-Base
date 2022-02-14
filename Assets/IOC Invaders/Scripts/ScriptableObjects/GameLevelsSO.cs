using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Levels Config", fileName = "New GameLevels")]
public class GameLevelsSO : ScriptableObject
{
    [SerializeField] private List<LevelConfigSO> levels;

    public List<LevelConfigSO> Levels
    {
        get { return levels; }
    }


    [SerializeField] private float timeBeforeStartLevel = 3f;

    public float TimeBeforeStartLevel
    {
        get { return timeBeforeStartLevel; }
    }
}