using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod : MonoBehaviour
{
    protected Player player;
    protected Rigidbody2D rig;
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = Player.playerInstence;
        rig = GetComponent<Rigidbody2D>();
        Equip();
    }

    public virtual void Equip()
    {

    }
    public virtual void Unequip()
    {

    }
}
