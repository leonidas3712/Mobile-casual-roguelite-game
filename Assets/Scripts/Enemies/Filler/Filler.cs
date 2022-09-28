using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filler : Enemy
{
    [SerializeField]
    float flightForce,speedLimit;
    [SerializeField]
    GameObject spikes;
    Vector2 flightDir;



    private void Update()
    {
        if(rig.velocity.magnitude<speedLimit)rig.AddForce(flightDir * flightForce);
        if (timer < Time.time)
        {
            timer = Time.time + Random.Range(2, 5);
            flightDir = HelpfulFuncs.Norm1(new Vector2(Random.Range(1, 10), Random.Range(1, 10)));
        }
    }
    void Spike()
    {
        spikes.SetActive(true);
        Invoke("CloseSpikes",0.6f);
    }
    void CloseSpikes()
    {
        spikes.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Player")
        {
            if (spikes.activeSelf == false)
            {
                CancelInvoke();
                Invoke("Spike", 0.1f);
            }
            else
            {
                PlayerHp.singleton.TakeDamage(1);
                //player.GetComponent<Rigidbody2D>().AddForce();
            }

        }
        else
        {
            flightDir = HelpfulFuncs.Norm1(Vector2.Reflect(flightDir, coll.GetContact(0).normal));
            timer = Time.time + Random.Range(3, 5);
        }
    }
}
