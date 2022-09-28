using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstExit : Exit
{
    [SerializeField]
    PermaStats perma;
    [SerializeField]
    GameObject playerCanvas;
    public override void Start()
    {
        hp = maxHp;
        reward = RoomsSingelton.instance.RewardsPrefabs[Random.Range(0, RoomsSingelton.instance.RewardsPrefabs.Count)];
        SpriteRenderer rewardRenderer = reward.GetComponent<SpriteRenderer>();
        rewardShower.sprite = rewardRenderer.sprite;
        rewardShower.color = rewardRenderer.color;

    }
    public override void Die()
    {
        perma.BeginRun();
        playerCanvas.SetActive(true);
        Player.playerInstence.GetComponent<PlayerHp>().UpdateText();
        base.Die();
    }


}

