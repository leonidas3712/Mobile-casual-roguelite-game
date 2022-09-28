using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy
{
    [SerializeField]
    GameObject bulletPrefab;
    GameObject bullet;
    [SerializeField]
    float bulletSpeed;

    public override void Start()
    {
        base.Start();
        GetComponent<Hp>().onTakeDamage += ResetLoad;
        timer = Time.time + 3;
    }
    void Update()
    {
        if (timer < Time.time)
        {
            Shoot();
            timer = Time.time + intervals;
        }
    }
    void Shoot()
    {
        bullet = (GameObject)Instantiate(bulletPrefab,transform.position,transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity =  HelpfulFuncs.Norm1(player.transform.position - transform.position) * bulletSpeed;
    }
    void ResetLoad()
    {
        timer = Time.time + intervals;
    }
}
