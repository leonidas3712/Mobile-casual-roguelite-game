using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxDamageIncrease : Reward
{
    public override void Die()
    {
        Player.playerInstence.damage+=2;

        base.Die();
    }
}
