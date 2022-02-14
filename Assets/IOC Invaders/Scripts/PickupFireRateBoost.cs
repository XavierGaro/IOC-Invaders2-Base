using UnityEngine;

public class PickupFireRateBoost : PickupAbstract
{
    [SerializeField] private float power = 0.02f;

    protected override void ProcessPickup(GameObject player)
    {
        WeaponPlayer weapon = player.GetComponent<WeaponPlayer>();

        weapon.IncreaseFireRate(power);
    }
}