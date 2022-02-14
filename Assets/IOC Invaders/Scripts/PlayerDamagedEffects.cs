using System;
using UnityEngine;

public class PlayerDamagedEffects : MonoBehaviour
{
    private CameraShake _cameraShake;


    private void Start()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        GetComponent<Health>().OnDeath += OnDeath;
        GetComponent<Health>().OnHealthChanged += OnHealthChanged;
    }


    private void OnHealthChanged(int amount)
    {
        if (amount < 0 && _cameraShake != null)
        {
            _cameraShake.Play();    
        }
    }

    private void OnDeath()
    {
        GameManager.LoadGameOver();
    }

    private void OnDestroy()
    {
        GetComponent<Health>().OnDeath -= OnDeath;
        GetComponent<Health>().OnHealthChanged -= OnHealthChanged;
    }
}