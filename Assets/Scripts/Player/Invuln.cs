using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invuln : MonoBehaviour
{

    public static Invuln singelton;

    [SerializeField]
    float intevals, time;
    float timer, colorTimer;

    [SerializeField]
    SpriteRenderer sr;

    [SerializeField]
    Collider2D coll;

    Color sColor;

    public static bool isInvuln;

    void Awake()
    {
        singelton = this;
        sColor = sr.color;
    }


    void Update()
    {
        if (!isInvuln) return;

        if (timer < Time.time)
        {
            End();
        }
        else
        {
            if (colorTimer < Time.time)
            {
                if (sr.color == Color.clear)
                    sr.color = sColor;
                else
                    sr.color = Color.clear;
                colorTimer = Time.time + intevals;
            }
        }
    }

    public void Execute()
    {
        isInvuln = true;
        Physics2D.IgnoreLayerCollision(10, 8, true);
        timer = Time.time + time;
    }
    void End()
    {

        Physics2D.IgnoreLayerCollision(10, 8, false);
        isInvuln = false;
        sr.color = sColor;
    }
}
