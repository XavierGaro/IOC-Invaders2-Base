using System.Collections;
using UnityEngine;

public abstract class WeaponAbstract : MonoBehaviour
{
    [Header("General")] [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float projectileSpeed = 10f;
    [SerializeField] protected float projectileLifetime = 5f;
    [SerializeField] protected float baseFireRate = 0.2f;

    [SerializeField] protected GameObject Cannon;

    [HideInInspector] public bool isFiring;

    [SerializeField] protected int damage;

    protected Coroutine firingCoroutine;
    protected AudioManager AudioManager;


    protected virtual void Awake()
    {
        AudioManager = FindObjectOfType<AudioManager>();
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

    protected abstract IEnumerator FireContinuously();
}