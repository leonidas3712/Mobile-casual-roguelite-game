using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : Enemy
{
    Vector2 flightDir;

    [SerializeField]
    protected float bulletSpeed, flightSpeed, maxFlightSpeed;

    [SerializeField]
    protected GameObject shellPrefab;

    public override void Start()
    {
        base.Start();
        flightDir = HelpfulFuncs.Norm1(new Vector3(Random.Range(10, 10), Random.Range(10, 10), 0));
        if (rig.velocity.magnitude > maxFlightSpeed)
        {
            flightDir = HelpfulFuncs.Norm1(rig.velocity);
            rig.velocity = flightDir * maxFlightSpeed;
        }
        else
        {
            rig.AddForce(flightDir*flightSpeed);
        }
        timer = Time.time + 3;
    }

    private void Update()
    {
        if (timer < Time.time)
        {
            Shoot();
            timer = Time.time + Random.Range(1, intervals);
        }
    }
    private void FixedUpdate()
    {
        rig.velocity = flightDir * flightSpeed;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag != "Player")
            flightDir = HelpfulFuncs.Norm1(Vector2.Reflect(flightDir, coll.GetContact(0).normal));
    }

    public virtual void Shoot()
    {
        Rigidbody2D[] rbs = Instantiate(shellPrefab, transform.position, transform.rotation).GetComponentsInChildren<Rigidbody2D>();
        rbs[0].velocity = new Vector2(1, 0) * bulletSpeed;
        rbs[1].velocity = new Vector2(-1, 0) * bulletSpeed;
    }
}
