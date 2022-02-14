using System.Collections;
using UnityEngine;

public class WeaponPlayer : WeaponAbstract
{
    void Start()
    {
        GameState.Instance.SetBase(damage, baseFireRate);

        // Actualitzem les dades de la nau amb les dades desades
        damage += GameState.Instance.GetPowerIncrease();
        baseFireRate -= GameState.Instance.GetFireRateIncrease();
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
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


            AudioManager.PlayShootingClip();

            yield return new WaitForSeconds(baseFireRate);
            //yield return new WaitForSeconds(baseFireRate);
        }
    }

    public void IncreaseFireRate(float increase)
    {
        baseFireRate -= increase;
        if (baseFireRate < PlayerController.MIN_FIRE_RATE)
        {
            baseFireRate = PlayerController.MIN_FIRE_RATE;
        }

        GameState.Instance.IncreaseFireRate(increase);
    }

    public void IncreaseDamage(int power)
    {
        damage += power;
        GameState.Instance.IncreaseDamage(power);
    }
}