using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int newDamage)
    {
        this.damage = newDamage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}