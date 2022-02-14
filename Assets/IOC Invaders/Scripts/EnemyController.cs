using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private WaveConfigSO _waveConfig;
    private List<Transform> _waypoints;
    private int _waypointIndex = 0;

    [SerializeField] private float speed = 5;

    void Awake()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        _waveConfig = _enemySpawner.GetCurrentWave();
        _waypoints = _waveConfig.GetWayPoints();
        transform.position = _waypoints[_waypointIndex].position;
    }

    void FixedUpdate()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (_waypointIndex < _waypoints.Count)
        {
            Vector3 targetPosition = _waypoints[_waypointIndex].position;

            float delta = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}