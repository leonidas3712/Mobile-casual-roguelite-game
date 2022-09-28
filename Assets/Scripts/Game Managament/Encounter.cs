using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    //public List<List<GameObject>> Waves = new List<List<GameObject>>();
    public GameObject nextEncounter;

    public GameObject GetNextFoe()
    {
        if (Enemies.Count == 0) return null;

        int rand = Random.Range(0, Enemies.Count - 1);
        GameObject temp = Enemies[rand];
        Enemies.Remove(temp);
        return temp;
    }
}
