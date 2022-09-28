using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Hp
{
    [SerializeField]
    GameObject room;
    Room myRoom;
    [SerializeField]
    protected SpriteRenderer rewardShower;
    GameObject nextRoomPrefab, nextRoom;
    protected GameObject reward;
    public override void Start()
    {
        base.Start();
        myRoom = Room.currentRoom;
        reward =myRoom.possibleRewards[Random.Range(0, myRoom.possibleRewards.Count)];
        SpriteRenderer rewardRenderer = reward.GetComponent<SpriteRenderer>();
        rewardShower.sprite = rewardRenderer.sprite;
        rewardShower.color = rewardRenderer.color;
        myRoom.possibleRewards.Remove(reward);
    }
    public override void Die()
    {
        nextRoomPrefab = RoomsSingelton.instance.RoomsPrefabs[Random.Range(0, RoomsSingelton.instance.RoomsPrefabs.Count - 1)];
        nextRoom = Instantiate(nextRoomPrefab, Vector3.zero, transform.rotation);
        RoomsSingelton.instance.RoomsPrefabs.Remove(nextRoomPrefab);
        nextRoom.GetComponent<Room>().SetReward(reward);
        Destroy(room);
    }


}
