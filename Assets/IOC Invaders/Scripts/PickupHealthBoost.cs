using UnityEngine;

public class PickupHealthBoost : PickupAbstract
{
    [SerializeField] private int power;

    protected override void ProcessPickup(GameObject player)
    {
        Health health = player.GetComponent<Health>();

        if (health != null)
        {
            health.Restore(power);
        }
    }
}