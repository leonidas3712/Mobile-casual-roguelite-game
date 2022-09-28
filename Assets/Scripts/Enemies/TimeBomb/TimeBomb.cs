using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : Enemy
{
    [SerializeField]
    float flightPower;

    [SerializeField]
    GameObject explosionPrefab;

    public override void Start()
    {
        base.Start();
        timer += Time.time;
    }
    void Update()
    {
        if (timer < Time.time)
            Explode();
        rig.AddForce(HelpfulFuncs.Norm1(player.transform.position - transform.position)*flightPower);
    }
    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
