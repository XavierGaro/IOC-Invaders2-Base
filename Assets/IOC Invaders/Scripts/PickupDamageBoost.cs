using UnityEngine;

public class PickupDamageBoost : PickupAbstract
{
    [SerializeField] private int power = 5;

    protected override void ProcessPickup(GameObject player)
    {
        WeaponPlayer weapon = player.GetComponent<WeaponPlayer>();

        weapon.IncreaseDamage(power);
    }
}