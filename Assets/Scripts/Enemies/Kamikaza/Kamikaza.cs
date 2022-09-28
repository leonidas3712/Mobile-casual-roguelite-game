using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaza : Enemy
{
    [SerializeField]
    float time, flightPower;
    public bool trrigered;
    Vector2 playerDir;
    int layerMask;

    [SerializeField]
    GameObject explosionPrefab;

    public override void Start()
    {
        base.Start();
        layerMask = ~LayerMask.GetMask("Enemies");
    }

    void Update()
    {
        playerDir = HelpfulFuncs.Norm1(player.transform.position - transform.position);
        if (timer < Time.time && trrigered)
            Explode();
        rig.AddForce(playerDir * flightPower);
        if (!trrigered)
        {
            RaycastHit2D castHit = Physics2D.Raycast(transform.position, playerDir, 2,layerMask);
            if (castHit)
            {
                if (castHit.collider.tag=="Player")
                {
                    timer = Time.time + time;
                    trrigered = true;
                }
            }
        }
    }
    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

