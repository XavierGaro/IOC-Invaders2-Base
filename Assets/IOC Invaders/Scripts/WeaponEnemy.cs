using System.Collections;
using UnityEngine;

public class WeaponEnemy : WeaponAbstract
{
    [SerializeField] private float firingRateVariance = 0f;
    [SerializeField] private float minimumFiringRate = 0.1f;

    void Start()
    {
        isFiring = true;
    }

    override protected IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, Cannon.transform.position, Cannon.transform.rotation);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Cannon.transform.up * projectileSpeed;
            }

            instance.GetComponent<DamageDealer>().SetDamage(damage);

            Destroy(instance, projectileLifetime);

            float timeToNextProjectile = Random.Range(baseFireRate - firingRateVariance
                , baseFireRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            AudioManager.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
            //yield return new WaitForSeconds(baseFireRate);
        }
    }
}