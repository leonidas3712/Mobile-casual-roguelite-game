using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : Hp
{
    public override void Die()
    {
        BasicCharge.singleton.IncreaseCombo(1);
        base.Die();
    }
}
