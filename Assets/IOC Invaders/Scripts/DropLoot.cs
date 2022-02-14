using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropLoot : MonoBehaviour
{

    [SerializeField] private List<GameObject> loot;
    [SerializeField] private float lootChance;

    private void Start()
    {
        GetComponent<Health>().OnDeath += Drop;
    }

    private void Drop()
    {
        float roll = Random.Range(0f, 1f);

        if (roll < lootChance)
        {
            int index = Random.Range(0, loot.Count);

            GameObject drop = Instantiate(loot[index], transform.position, Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        GetComponent<Health>().OnDeath -= Drop;
    }
}
