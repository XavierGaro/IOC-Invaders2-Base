using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health playerHealth;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI fireRateText;
    [SerializeField] private TextMeshProUGUI levelText;
    
    private LevelManager _levelManager;

    [Header("Animations")]
    [SerializeField] Animation scoreAnimation;
    [SerializeField] Animation healthAnimation;
    
    void Start()
    {
        GameState.Instance.OnScoreChanged += UpdateScore;
        GameState.Instance.OnStatsChanged += UpdateStats;

        _levelManager = FindObjectOfType<LevelManager>();
        if (levelText != null)
        {
            levelText.text = _levelManager.GetCurrentLevelTitle();    
        }

        if (healthSlider !=null)
        {
            healthSlider.maxValue = playerHealth.GetHealth();
            healthSlider.value = healthSlider.maxValue;
    
        }

        if (scoreText != null)
        {
            scoreText.text = GameState.Instance.GetScore().ToString();
        }

        
        
        playerHealth.OnHealthChanged += UpdateHealth;


        UpdateStats();
    }

    void UpdateHealth(int amount)
    {
        if (healthSlider == null)
        {
            return;
        }
        
        
        int newHealth = playerHealth.GetHealth();

        healthSlider.value = newHealth;

        if (amount > 0)
        {
            healthAnimation.Play();
        }
    }

    void UpdateStats()
    {
        
        // Bales per minut
        int bulletsPerMinute = (int)(1f / GameState.Instance.GetFireRate() * 60f);

        if (fireRateText != null)
        {
            fireRateText.text = "Fire Rate: " + bulletsPerMinute + "rpm";    
        }

        if (powerText != null)
        {
            powerText.text = "Power: " + GameState.Instance.GetPower();    
        }
        
    }

    void UpdateScore()
    {
        if (scoreAnimation != null)
        {
            scoreAnimation.Play();    
        }

        if (scoreText != null)
        {
            scoreText.text = GameState.Instance.GetScore().ToString();    
        }
        
    }

    private void OnDestroy()
    {
        // Com que _gameState és un singleton, si no ens desuscribim quan
        // es canvia de nivell continua intentant actualitzar la UI antiga
        GameState.Instance.OnScoreChanged -= UpdateScore;
        GameState.Instance.OnStatsChanged -= UpdateStats;
    }
}