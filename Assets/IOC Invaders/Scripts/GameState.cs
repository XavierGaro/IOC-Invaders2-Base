using UnityEngine;

public class GameState : MonoBehaviour
{
    public delegate void OnEventDelegate();

    public event OnEventDelegate OnScoreChanged;
    public event OnEventDelegate OnStatsChanged;


    private int _score;
    private float _fireRate;
    private int _power;
    private int _basePower;
    private float _baseFireRate;

    private static GameState _instance;

    public static GameState Instance
    {
        get { return _instance; }
    }

    private int _currentLevel;


    public int CurrentLevel
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    private void Awake()
    {
        
        if (_instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return _score;
    }

    public void IncreaseScore(int value)
    {
        _score += value;
        Mathf.Clamp(_score, 0, int.MaxValue);

        if (OnScoreChanged != null) OnScoreChanged();
    }

    public void Reset()
    {
        _score = 0;
        _power = 0;
        _fireRate = 0;
        _currentLevel = 0;
    }

    public float GetFireRate()
    {
        return _baseFireRate - _fireRate;
    }

    public int GetPower()
    {
        return _basePower + _power;
    }

    public void IncreaseDamage(int power)
    {
        this._power += power;
        if (OnStatsChanged != null) OnStatsChanged();
    }

    public void IncreaseFireRate(float increase)
    {
        float currentFireRate = _baseFireRate - _fireRate;
        if (currentFireRate <= PlayerController.MIN_FIRE_RATE)
        {
            return;
        }

        if (currentFireRate - increase < PlayerController.MIN_FIRE_RATE)
        {
            increase = currentFireRate - PlayerController.MIN_FIRE_RATE;
        }

        _fireRate += increase;

        if (OnStatsChanged != null) OnStatsChanged();
    }

    public void SetBase(int power, float fireRate)
    {
        _basePower = power;
        _baseFireRate = fireRate;
        if (OnStatsChanged != null) OnStatsChanged();
    }

    public int GetPowerIncrease()
    {
        return _power;
    }

    public float GetFireRateIncrease()
    {
        return _fireRate;
    }
}