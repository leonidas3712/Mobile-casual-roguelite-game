using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : Hp
{
    public static PlayerHp singleton;

    [SerializeField]
    public Text text;
    public int baseMaxHp;

    [SerializeField]
    public GameObject healParticles;

    public override void Start()
    {
        base.Start();
        singleton = this;
    }


    public override void Die()
    {
        SceneManager.LoadScene("Game");
    }
    public override void TakeDamage(int damage)
    {
        if (Invuln.isInvuln) return;
        BasicCharge.singleton.ResetCombo();
        Room.currentRoom.cameraAnimator.SetTrigger("PlayerHitShake");
        base.TakeDamage(damage);
        UpdateText();
        Invuln.singelton.Execute();
    }

    public virtual void Heal(int cur, int max = 0)
    {
        maxHp += max;
        hp += cur;
        if (hp > maxHp) hp = maxHp;
        Destroy(Instantiate(healParticles, transform.position, Quaternion.Euler(0, 0, 0)), 1f);
        UpdateText();
    }
    public virtual void UpdateText()
    {
        if (text)
            text.text = "Hp " + hp + " / " + maxHp;
    }
    private void Update()
    {
        if (!text.canvas.worldCamera) text.canvas.worldCamera = Room.currentRoom.cameraAnimator.GetComponent<Camera>();
    }
}

