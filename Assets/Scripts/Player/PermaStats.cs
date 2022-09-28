using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermaStats : MonoBehaviour
{
    int sp, hp, dam;
    [SerializeField]
    PlayerHp playerHp;
    [SerializeField]
    Text HPText, DamText, SPText;
    private void Awake()
    {
        hp = PlayerPrefs.GetInt("PlayerMaxHp", -1);
        if (hp == -1)
        {
            PlayerPrefs.SetInt("PlayerMaxHp", 0);
            hp = 0;
        }

        dam = PlayerPrefs.GetInt("PlayerDamage", -1);
        if (dam == -1)
        {
            PlayerPrefs.SetInt("PlayerDamage", 0);
            dam = 0;
        }


        sp = PlayerPrefs.GetInt("PlayerSP", -1);
        if (sp == -1)
        {

            PlayerPrefs.SetInt("PlayerSP", 0);
            sp = 0;
        }
        UpdateDamText();
        UpdateHPText();
        UpdateSPText();

    }
    void UpdateHPText()
    {
        HPText.text = "HP bonus " + hp;
    }
    void UpdateDamText()
    {
        DamText.text = "Damage bonus " + dam;
    }
    void UpdateSPText()
    {
        SPText.text = "SP " + sp;
    }

    public void IncreaseHP()
    {
        if (sp < 5) return;
        hp++;
        sp -= 5;
        UpdateSPText();
        UpdateHPText();

    }
    public void DecreaseHP()
    {
        if (hp == 0) return;
        hp--;
        sp += 5;
        UpdateSPText();
        UpdateHPText();
    }

    public void IncreaseDamage()
    {
        if (sp < 25) return;
        dam++;
        sp -= 25;
        UpdateDamText();
        UpdateSPText();
    }
    public void DecreaseDamage()
    {
        if (dam == 0) return;
        dam--;
        sp += 25;
        UpdateDamText();
        UpdateSPText();
    }

    public void BeginRun()
    {
        PlayerPrefs.SetInt("PlayerMaxHp", hp);
        PlayerPrefs.SetInt("PlayerDamage", dam);
        PlayerPrefs.SetInt("PlayerSP", sp);
        PlayerPrefs.Save();

        Player.playerInstence.damage = Player.playerInstence.baseDamage + dam;
        playerHp.maxHp = playerHp.baseMaxHp + hp;
        playerHp.hp = playerHp.maxHp;
    }
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        hp = 0;
        dam = 0;
        sp = 0;
        UpdateDamText();
        UpdateHPText();
        UpdateSPText();
    }
}
