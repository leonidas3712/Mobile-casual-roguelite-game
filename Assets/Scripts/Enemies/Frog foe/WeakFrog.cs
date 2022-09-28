using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakFrog : Enemy
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

    bool grounded;

    public override void Start()
    {
        base.Start();
        gravity = rig.gravityScale;
        myColl = GetComponent<Collider2D>();
        layerMask = ~LayerMask.GetMask("Enemies", "HitBox");
        scale = sprite.transform.localScale;
        timer = Time.time + 3;
        myHP.onTakeDamage += Drop;
    }
    private void Update()
    {
        if (timer < Time.time&&grounded)
        {
            Dash();
            timer = Time.time + intervals+Random.Range(0.5f,1);
            grounded = false;
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

        if (hit.collider.tag == "Player")
        {
            rig.velocity = (Vector2)(HelpfulFuncs.Norm1((player.transform.position - transform.position)) * dashStrength) + stickNormal * dashStrength / 8;
        }
        else
        {
            rig.velocity = RandomDashDiraction() * dashStrength;
        }

    }
    Vector3 RandomDashDiraction()
    {
        Vector3 randomDir = new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0);
        if (randomDir.x * stickNormal.x < 0) randomDir = new Vector3(randomDir.x * -1, randomDir.y, 0);
        if (randomDir.y * stickNormal.y < 0) randomDir = new Vector3(randomDir.x, randomDir.y * -1, 0);
        return HelpfulFuncs.Norm1(randomDir);
    }

    void Stick()
    {
        sprite.transform.localScale = scale;
        rig.gravityScale = 0;
        rig.velocity *= 0;
        timer = Time.time + intervals+Random.Range(0.5f, 1);
        grounded = true;
    }
    void Unstick()
    {
        rig.gravityScale = gravity;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (isAttacking&&!player.isAttacking)
        {
            if (coll.collider.tag == "Player")
            {
                player.GetComponent<Hp>().TakeDamage(1);
            }
            else
            if (coll.collider.tag != "Foe" && coll.collider.tag != "HitBox")
            {
                Stick();
            }
            sprite.transform.localScale = scale;
            isAttacking = false;
        }
        else
        if (coll.collider.tag == "Untagged")
        {
            stickNormal = coll.GetContact(0).normal;
            Stick();
        }
    }

    void Drop()
    {
        sprite.transform.localScale = scale;
        timer = Time.time + intervals;
        rig.velocity *= 0.3f;
        isAttacking = false;
    }

    void SetRotation()
    {
        tiltDir = HelpfulFuncs.Norm1(rig.velocity);
        sprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(tiltDir.x, tiltDir.y) * Mathf.Rad2Deg * -1);
    }
}
