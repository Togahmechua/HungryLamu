using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProceduralSpawner : MonoBehaviour
{
    [Header("Spawn Areas")]
    [SerializeField]
    private Transform[] spawnPoints;

    [Header("Items")]
    [SerializeField]
    private GameObject[] items;

    private List<Transform> possibleSpawns = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            possibleSpawns.Add(spawnPoints[i]);
        }
        SpawnItems();
    }

    public void SpawnItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            int index = Random.Range(0, possibleSpawns.Count);
            items[i].transform.position = possibleSpawns[index].position;
            possibleSpawns.RemoveAt(index);
        }
    }
}
