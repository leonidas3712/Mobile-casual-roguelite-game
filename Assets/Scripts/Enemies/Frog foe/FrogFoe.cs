using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogFoe : Enemy
{
    float gravity;
    [SerializeField]
    float dashStrength;
    bool isAttacking;
    [SerializeField]
    GameObject hitbox, sprite;

    Vector3 scale;

    int layerMask;
    Collider2D myColl;

    Vector2 stickNormal, tiltDir;

    public override void Start()
    {
        base.Start();
        hitbox.GetComponent<HitBox>().Hit += Hit;
        gravity = rig.gravityScale;
        myColl = GetComponent<Collider2D>();
        layerMask = ~LayerMask.GetMask("Enemies", "HitBox");
        scale = sprite.transform.localScale;
        timer = Time.time + 3;
    }
    private void Update()
    {
        if (timer < Time.time)
        {
            Dash();
            timer = Time.time + intervals;
        }
        if (isAttacking)
        {
            SetRotation();
        }
    }
    void Dash()
    {
        isAttacking = true;
        sprite.transform.localScale = new Vector3(scale.x * 0.7f, scale.y * 1.3f, 1);
        Unstick();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, HelpfulFuncs.Norm1(player.transform.position - transform.position), 100, layerMask);
        print(hit.collider.tag);
        if (hit.collider.tag == "Player")
        {
            rig.velocity = (Vector2)(HelpfulFuncs.Norm1((player.transform.position - transform.position)) * dashStrength) + stickNormal * dashStrength / 8;
        }
        else
        {
            rig.velocity = RandomDashDiraction() * dashStrength;
        }
        hitbox.SetActive(true);
        myColl.enabled = false;
    }
    Vector3 RandomDashDiraction()
    {
        Vector3 randomDir = new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0);
        if (randomDir.x * stickNormal.x < 0) randomDir = new Vector3(randomDir.x * -1, randomDir.y, 0);
        if (randomDir.y * stickNormal.y < 0) randomDir = new Vector3(randomDir.x, randomDir.y * -1, 0);
        return HelpfulFuncs.Norm1(randomDir);
    }
    void Hit(Collision2D coll)
    {
        if (coll.collider.tag == "Player")
        {
            hitbox.SetActive(false);
            myColl.enabled = true;
            player.GetComponent<Hp>().TakeDamage(1);
        }
        else
        if (coll.collider.tag != "Foe" && coll.collider.tag != "HitBox")
        {
            hitbox.SetActive(false);
            myColl.enabled = true;
            Stick();
        }
        sprite.transform.localScale = scale;
        isAttacking = false;
    }

    void Stick()
    {
        rig.gravityScale = 0;
        rig.velocity *= 0;
        timer = Time.time + intervals;
    }
    void Unstick()
    {
        rig.gravityScale = gravity;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Untagged")
        {
            stickNormal = coll.GetContact(0).normal;
            Stick();
        }
    }

    void SetRotation()
    {
        tiltDir = HelpfulFuncs.Norm1(rig.velocity);
        sprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(tiltDir.x, tiltDir.y) * Mathf.Rad2Deg * -1);
    }
}
