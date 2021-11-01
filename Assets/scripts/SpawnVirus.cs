using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnVirus : MonoBehaviour
{

    public static SpawnVirus Singletone;
    
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private float spawnRange;

    private void Start()
    {
        Singletone = this;
    }

    public void Spawn() // Spawn Random prefab
    {
        GameObject currentPrefab = prefabs[Random.Range(0, prefabs.Length)];
        Vector3 needPos = new Vector3(Random.Range(-spawnRange, spawnRange), transform.position.y, transform.position.z);
        Instantiate(currentPrefab, needPos, Quaternion.identity);
    }

    private void OnDrawGizmos() // Visible spawn range
    {
        Vector3 from = new Vector3(spawnRange, transform.position.y, transform.position.z);
        Vector3 to = new Vector3(-spawnRange, transform.position.y, transform.position.z);
        Gizmos.DrawLine(from, to);
    }
}