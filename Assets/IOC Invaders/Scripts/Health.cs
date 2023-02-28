using UnityEngine;

public class Health : MonoBehaviour
{
    // Definim els delegats per detectar quan es destrueix l'objecte, quan es mort i quan canvien els punts de vida 
    public delegate void OnEventDelegate();

    public event OnEventDelegate OnDestroyEvent;

    public event OnEventDelegate OnDeath;
    
    public delegate void OnHealthChangeDelegate(int amount);

    public event OnHealthChangeDelegate OnHealthChanged;


    
    
    [SerializeField] private int maxHealth = 50;
    private int _currentHealth;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem exploEffect;


    protected virtual void Awake()
    {
        _currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            ProcessImpact(damageDealer);
        }
    }

    protected virtual void ProcessImpact(DamageDealer damageDealer)
    {
        TakeDamage(damageDealer.GetDamage());
        AudioManager.Instance.PlayDamageClip();
        damageDealer.Hit();
    }

    private void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, int.MaxValue);

        if (OnHealthChanged != null) OnHealthChanged(-damage);

        if (_currentHealth <= 0)
        {
            PlayEffect(exploEffect);
            Die();
            Destroy(gameObject);
        }
        else
        {
            PlayEffect(hitEffect);
        }
    }

    // Les subclasses han de cridar al pare amb base.OnDeath() o no es dispararà l'esdeveniment
    protected virtual void Die()
    {
        if (OnDeath != null) OnDeath();
    }

    void PlayEffect(ParticleSystem effect)
    {
        if (effect != null)
        {
            // TODO: Exercici 4c
            
        }
    }

    public int GetHealth()
    {
        return _currentHealth;
    }

    public void Restore(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        if (OnHealthChanged != null) OnHealthChanged(amount);
    }


    private void OnDestroy()
    {
        // Send notification that this object is about to be destroyed
        if (OnDestroyEvent != null)
        {
            OnDestroyEvent();
        }
    }
}