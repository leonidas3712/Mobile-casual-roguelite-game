using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : Reward
{
    public override void Die()
    {
        Mod mod = RoomsSingelton.instance.mods[Random.Range(0, RoomsSingelton.instance.mods.Count - 1)];
        Player.playerInstence.gameObject.AddComponent(mod.GetType());
        RoomsSingelton.instance.mods.Remove(mod);
        base.Die();
    }
}
