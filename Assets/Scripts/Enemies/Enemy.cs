using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Player player;
    protected Rigidbody2D rig;

    protected float timer = 2;
    [SerializeField]
    protected float intervals;
    [SerializeField]
    protected bool shouldStagger;
    public Hp myHP;

    public void ResetTimer()
    {
        if (shouldStagger)
            timer = Time.time + intervals;
    }

    public virtual void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = new Vector2(Random.Range(0,3), Random.Range(0, 3))*10;
        player = Player.playerInstence;
        if (myHP)
            myHP.onTakeDamage += ResetTimer;
    }

}
