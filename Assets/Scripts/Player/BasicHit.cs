using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHit : Mod
{
    public static BasicHit singelton;
    float gravityScale;
    [SerializeField]
    int baseDamage = 1;
    public int curDamage;
    Animator camAnimator;
    [SerializeField]
    public GameObject damageNumber, critDamageNumber;




    public override void Start()
    {
        base.Start();
        gravityScale = rig.gravityScale;
    }
    public override void Equip()
    {
        if (singelton)
        {
            damageNumber = singelton.damageNumber;
            singelton.Unequip();
        }
        singelton = this;
        player.onWalled += HitWall;
        player.onUnWall += UnWall;
        player.onHit += HitFoe;
        player.whileAttaking += CalculateDamage;
    }
    public override void Unequip()
    {
        player.onWalled -= HitWall;
        player.onUnWall -= UnWall;
        player.onHit -= HitFoe;
        player.whileAttaking -= CalculateDamage;
        base.Unequip();
        Destroy(this);
    }

    public virtual void HitFoe(GameObject foe)
    {
        player.ResetTimesDone();
        Hp hp = foe.GetComponent<Hp>();
        hp.TakeDamage(curDamage);
        Vector2 dir = HelpfulFuncs.Norm1(foe.transform.position - transform.position);

        if (hp.hitParticalsPrefab)
        {
            ParticleSystem ps = Instantiate(hp.hitParticalsPrefab, foe.transform.position, Quaternion.Euler(Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg - 90, 90, 0)).GetComponent<ParticleSystem>();
            /*var main = ps.main;
            main.startColor = hp.sr.color;*/
        }
        if (curDamage < 4)
            Instantiate(damageNumber, foe.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<DamageNumber>().SetNumber(curDamage, dir);
        else
            Instantiate(critDamageNumber, foe.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<DamageNumber>().SetNumber(curDamage, dir);

        Room.currentRoom.cameraAnimator.SetTrigger("HitShake");
       

    }
    void CalculateDamage()
    {
        int vel = (int)rig.velocity.magnitude;
        curDamage = 1;
        if (vel > 40) curDamage = 2;
        if (vel > 50) curDamage = 3;
        if (vel >= 60)
        {
            curDamage = (vel - 60) / 5;
            if (curDamage > player.damage) curDamage = player.damage;
            curDamage += 4;
        }
    }
    void HitWall()
    {
        rig.gravityScale = 0;
        rig.velocity *= 0;
        player.walled = true;
    }
    void UnWall()
    {
        rig.gravityScale = gravityScale;
        player.walled = false;
    }

}