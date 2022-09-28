using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Kamikaza kam = coll.GetComponent<Kamikaza>();
        if (kam) kam.Explode();
        else
        {
            Hp hp = coll.GetComponent<Hp>();
            if (hp) hp.TakeDamage(damage);
        }
    }
    private void Start()
    {
        Invoke("Finish", 0.4f);
    }
    void Finish()
    {
        Destroy(gameObject);
    }
}
