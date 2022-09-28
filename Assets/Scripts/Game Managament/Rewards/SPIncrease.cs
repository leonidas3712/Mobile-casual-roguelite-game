using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPIncrease : Reward
{
    public override void Die()
    {
        print("look at me  " + PlayerPrefs.GetInt("PlayerSP"));
        PlayerPrefs.SetInt("PlayerSP",PlayerPrefs.GetInt("PlayerSP", 0)+2);
        PlayerPrefs.Save();
        base.Die();
    }
}
