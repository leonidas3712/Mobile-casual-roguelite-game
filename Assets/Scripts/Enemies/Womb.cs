using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Womb : Enemy
{
    [SerializeField]
    float  flightPower;
    [SerializeField]
    GameObject offsprings;

    public override void Start()
    {
        base.Start();
        timer = Time.time+intervals;
    }

    void Update()
    {
        if (timer < Time.time)
            Explode();
        
        rig.AddForce(HelpfulFuncs.Norm1(player.transform.position - transform.position) * flightPower);
    }
    void Explode()
    {
        Destroy(gameObject);
        Instantiate(offsprings,transform.position,transform.rotation);
    }
}
