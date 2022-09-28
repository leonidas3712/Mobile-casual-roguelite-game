using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    GameObject entrance, exits, reward;
    GameObject player;
    [SerializeField]
    List<Transform> spawnPoints = new List<Transform>();

    public Animator cameraAnimator;

    public static Room currentRoom;

    public Encounter encounter;

    bool encounterActive;

    public List<GameObject> possibleRewards = new List<GameObject>();

    void Start()
    {
        currentRoom = this;
        player = Player.playerInstence.gameObject;

        int count = RoomsSingelton.instance.EncounterPrefabs.Count - 1;
        GameObject temp = RoomsSingelton.instance.EncounterPrefabs[Random.Range(0, count)];
        encounter = Instantiate(temp, transform).GetComponent<Encounter>();
        RoomsSingelton.instance.EncounterPrefabs.Remove(temp);
        EnterRoom();

    }
    public void SetReward(GameObject RewardPrefab)
    {
        possibleRewards.AddRange(RoomsSingelton.instance.RewardsPrefabs);
        possibleRewards.Remove(RewardPrefab);
        reward = Instantiate(RewardPrefab, reward.transform.position, transform.rotation);
        reward.GetComponent<Reward>().room = this;
    }

    private void Update()
    {
        if (encounterActive && !GameObject.FindGameObjectWithTag("Foe"))
        {
            if (encounter.nextEncounter)
            {
                encounter =Instantiate(encounter.nextEncounter,transform).GetComponent<Encounter>();
                encounterActive = false;
                Invoke("SpawnWave", 0.8f);
            }
            else
                FinishEncaunter();
        }
    }


    void EnterRoom()
    {
        player.transform.position = entrance.transform.position;
        Invoke("SpawnWave",0.5f);
    }

    void SpawnWave()
    {
        encounterActive = true;
        while (encounter.Enemies.Count != 0)
        {
            Instantiate(encounter.GetNextFoe(), spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position, transform.rotation);
        }
    }

    void FinishEncaunter()
    {

        reward.SetActive(true);
        encounterActive = false;
    }

    public void ShowExits()
    {
        exits.SetActive(true);
    }
}
