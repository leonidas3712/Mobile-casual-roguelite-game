using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_Mod : Mod
{
    public int healAmount = 1;
    BasicHit hit;
    public override void Equip()
    {
        hit = GetComponent<BasicHit>();
        Player.playerInstence.onHit += Heal;

    }
    void Heal(GameObject foe)
    {
        if (!hit) hit = GetComponent<BasicHit>();
        int rand = Random.Range(0, 1);
        if (hit.curDamage > 3 && rand == 0)
            PlayerHp.singleton.Heal(healAmount);
    }
}
