using System;
using UnityEngine;

public class PlayerDamagedEffects : MonoBehaviour
{
    private CameraShake _cameraShake;


    private void Start()
    {
        // TODO: exercicici 4a (obtenir la referència al component CameraShake i afegir subscripcions)
        
    }


    private void OnHealthChanged(int amount)
    {
        if (amount < 0 && _cameraShake != null)
        {
            // TODO: Exercici 4a (tremolor de la càmera)

        }
    }

    private void OnDeath()
    {
        GameManager.LoadGameOver();
    }

    private void OnDestroy()
    {
        // TODO: Exercici 4a (eliminar subscripcions)
        
    }
}