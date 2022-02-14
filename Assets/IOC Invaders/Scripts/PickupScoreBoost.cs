using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class PickupScoreBoost : PickupAbstract
{
    [SerializeField] private int power;  


    protected override void ProcessPickup(GameObject player)
    {
        GameState.Instance.IncreaseScore(power);
    }
}