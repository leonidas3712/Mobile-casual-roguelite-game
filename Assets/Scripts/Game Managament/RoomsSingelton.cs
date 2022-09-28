using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsSingelton : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> RoomsPrefabs = new List<GameObject>(),EncounterPrefabs = new List<GameObject>(),RewardsPrefabs = new List<GameObject>();
    [SerializeField]
    public List<Mod> mods = new List<Mod>(); 

    public static RoomsSingelton instance;
    private void Awake()
    {
        instance = this;
    }
}
