using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : Hp
{
    [SerializeField]
    public Room room;
    public override void Die()
    {
        room.ShowExits();
        base.Die();
    }
}
