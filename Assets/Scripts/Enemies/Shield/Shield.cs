using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Enemy
{
    [SerializeField]
    GameObject shield, bulletPrefab;

    [SerializeField]
    float shieldInterval, shieldDuration, bulletSpeed;
    float shieldTimer;

    GameObject bullet;

    Vector2 playerDir;
    int layerMask;

    public override void Start()
    {
        base.Start();
        layerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        playerDir = HelpfulFuncs.Norm1(player.transform.position - transform.position);
        
        if(Physics2D.Raycast(transform.position, playerDir, 3, layerMask) && shieldTimer < Time.time && player.isAttacking)
        {
            RaiseShield();
        }
        if (shield.activeInHierarchy && shieldTimer < Time.time) LowerShield();

        if (timer < Time.time && !shield.activeInHierarchy)
        {
            Shoot();
        }

    }

    void RaiseShield()
    {
        shield.SetActive(true);
        shield.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerDir.x, playerDir.y) * Mathf.Rad2Deg * -1);
        shieldTimer = Time.time + shieldDuration;
        timer = 0;
    }
    void LowerShield()
    {
        shield.SetActive(false);
        shieldTimer = Time.time + shieldInterval;
    }
    void Shoot()
    {
        bullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = HelpfulFuncs.Norm1(player.transform.position - transform.position) * bulletSpeed;
        timer = Time.time + intervals;
    }
}
