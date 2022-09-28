using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleDash : Mod
{
    public override void Equip()
    {
        player.maxTimesDone = 3;
    }
}
