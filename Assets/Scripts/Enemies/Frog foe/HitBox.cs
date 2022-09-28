using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public delegate void FoeHit(Collision2D coll);
    public FoeHit Hit;


    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
        Hit(coll);
    }
}
