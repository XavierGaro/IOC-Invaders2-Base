using UnityEngine;

abstract public class PickupAbstract : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    [SerializeField] private ParticleSystem applyEffect;
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private float velocity = 2f;


    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.down * velocity;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.gameObject.GetComponent<Health>();

        if (health != null)
        {
            ProcessPickup(other.gameObject);
            AudioManager.Instance.PlayClip(sound);
            Destroy(gameObject);
        }
    }


    protected abstract void ProcessPickup(GameObject player);


    private void PlayEffect()
    {
        if (applyEffect != null)
        {
            ParticleSystem instance = Instantiate(applyEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }


    private void OnDestroy()
    {
        PlayEffect();
    }
}