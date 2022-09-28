using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicCharge : Mod
{
    public static BasicCharge singleton;
    [SerializeField]
    float strength, wallBoost;
    float angle, exstraSpeed, preSpeedFactor;
    [SerializeField]
    GameObject particlesPrefab;

    public float timeScale = 0.3f, fixedTimeScale = 0.02f * 0.3f;

    public int maxCombo;
    public int combo,comboKillCounter;

    [SerializeField]
    TextMeshProUGUI comboText;
    public GameObject comboTextPrefab;
    

    void StartAim()
    {
        CancelInvoke("SlowTime");
        Invoke("SlowTime",0.15f);
    }
    void SlowTime()
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = fixedTimeScale;
        CancelInvoke("SlowTime");
    }
    void Release()
    {
        angle = Vector2.Angle(rig.velocity, player.aimDir);
        preSpeedFactor = 1 - (angle / 180);

        exstraSpeed = preSpeedFactor * rig.velocity.magnitude;

        angle = Vector2.Angle(player.preCollVel, player.aimDir);
        preSpeedFactor = 1 - (angle / 180);

        exstraSpeed += preSpeedFactor * player.preCollVel.magnitude;

        if (player.walled)
        {
            exstraSpeed += wallBoost;
        }
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        CancelInvoke("SlowTime");
        rig.velocity = player.aimDir * (strength + exstraSpeed+combo*5);

        if (rig.velocity.magnitude > 60)
        {
            Instantiate(particlesPrefab, transform.position, transform.rotation);
        }
        angle = Vector2.Angle(player.wallNormal, player.aimDir);
        if (angle >= 90&&player.walled)
        {
            player.canStick = false;
            CancelInvoke("reEnableStick");
            Invoke("reEnableStick",0.1f);
        }
    }

    void reEnableStick()
    {
        player.canStick = true;
    }

    public void IncreaseCombo(int amount)
    {
        combo += amount;
        comboKillCounter += amount;
        if (combo > maxCombo) combo = maxCombo;
        CancelInvoke("ResetCombo");
        Invoke("ResetCombo", 3f);
        comboText.SetText("Combo X "+comboKillCounter);
        //Instantiate(comboTextPrefab, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<ComboComponent>().SetNumber(comboKillCounter, Vector2.up);
    }
    public void ResetCombo()
    {
        combo = 0;
        comboKillCounter = 0;
        comboText.SetText("");
    }

    public override void Equip()
    {
        player.onFingerDown += StartAim;
        player.onReleaseFinger += Release;
        singleton = this;
    }
}
