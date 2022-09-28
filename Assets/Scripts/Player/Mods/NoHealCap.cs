using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoHealCap : Mod
{

    public override void Equip()
    {
        PlayerHpNoCap noCap = player.gameObject.AddComponent<PlayerHpNoCap>();
        PlayerHp temp = PlayerHp.singleton;
        noCap.healParticles = temp.healParticles;
        noCap.baseMaxHp = temp.baseMaxHp;
        noCap.maxHp = temp.maxHp;
        noCap.hp = temp.hp;
        noCap.text = temp.text;
        PlayerHp.singleton = noCap;
        Destroy(temp);
        noCap.Heal(10);
        noCap.UpdateText();
    }

}
