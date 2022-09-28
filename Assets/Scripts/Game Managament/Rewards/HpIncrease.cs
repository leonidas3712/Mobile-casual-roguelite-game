using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpIncrease : Reward
{
    public override void Die()
    {
        PlayerHp.singleton.Heal(5, 3);

        base.Die();
    }
}
