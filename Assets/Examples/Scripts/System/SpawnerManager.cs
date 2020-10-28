using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public List<Spawner> spawners;

    private void Awake()
    {
        spawners = new List<Spawner>(FindObjectsOfType<Spawner>());
    }

    void Start()
    {
        foreach(var s in spawners)
        {
            s.StartSpawner();
        }
    }
}
