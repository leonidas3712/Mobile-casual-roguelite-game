using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<Hp>().TakeDamage(1);
        }
        
        print(coll.tag);
        Destroy(gameObject);
    }
}
