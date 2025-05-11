using Mono.Cecil.Cil;
using System.Threading;
using UnityEngine;

public class BombSpawnScript : MonoBehaviour
{
    public GameObject Bomb;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;
    public BoxCollider2D spawnArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnBomb", 0f, Random.Range(minSpawnTime, maxSpawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3 (x, y, 0f);
    }

    void SpawnBomb()
    {
        Instantiate(Bomb, GetRandomPosition(), Quaternion.identity);
        CancelInvoke("SpawnBomb");
        InvokeRepeating("SpawnBomb", Random.Range(minSpawnTime, maxSpawnTime), Random.Range(minSpawnTime, maxSpawnTime));
    }
}
