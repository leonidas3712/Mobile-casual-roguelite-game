using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTime : Mod
{
    BasicCharge charge;
    void Update()
    {
        if (!charge)
        {
            charge = GetComponent<BasicCharge>();
            charge.timeScale = 0.1f;
            charge.fixedTimeScale = 0.02f* 0.1f;
        }
    }
}
