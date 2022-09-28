using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterBig : Floater
{
    public override void Shoot()
    {
        Rigidbody2D[] rbs = Instantiate(shellPrefab, transform.position, transform.rotation).GetComponentsInChildren<Rigidbody2D>();
        rbs[0].velocity = new Vector2(1, 1) * bulletSpeed;
        rbs[1].velocity = new Vector2(-1, -1) * bulletSpeed;
        rbs[2].velocity = new Vector2(-1, 1) * bulletSpeed;
        rbs[3].velocity = new Vector2(1, -1) * bulletSpeed;
    }
}
