using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpNoCap : PlayerHp
{
    public override void Start()
    {
        
    }
    public override void Heal(int cur, int max = 0)
    {
        maxHp += max;
        hp += cur;
        Destroy(Instantiate(healParticles, transform.position, Quaternion.Euler(0, 0, 0)), 1f);
        UpdateText();
    }
    public override void UpdateText()
    {
        if (text)
            text.text = "Hp " + hp;
    }
}
