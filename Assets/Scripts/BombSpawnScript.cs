using Mono.Cecil.Cil;
using System.Threading;
using UnityEngine;

public class BombSpawnScript : MonoBehaviour
{
    public GameObject Bomb;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;
    public BoxCollider2D spawnArea;
    private bool finalBombSpawned = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerScript.Instance.commenceBombSpawn && GameManagerScript.Instance.gameStarted)
        {
            GameManagerScript.Instance.commenceBombSpawn = true;
            InvokeRepeating("SpawnBomb", 0f, Random.Range(minSpawnTime, maxSpawnTime));
        }
        if (GameManagerScript.Instance.finalBomb && !finalBombSpawned)
        {
            SpawnFinalBomb();
            finalBombSpawned = true;
        }
    }

    Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3 (x, y, 0f);
    }

    Vector3 GetFinalBombPosition()
    {
        float x = 9.03f;
        float y = 0f;
        return new Vector3(x, y, 0f);
    }

    void SpawnBomb()
    {
        if (GameManagerScript.Instance.gameStarted)
        {
            Instantiate(Bomb, GetRandomPosition(), Quaternion.identity);
            CancelInvoke("SpawnBomb");
            InvokeRepeating("SpawnBomb", Random.Range(minSpawnTime, maxSpawnTime), Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    void SpawnFinalBomb()
    {
        Instantiate(Bomb, GetFinalBombPosition(), Quaternion.identity);
    }

    private void OnDisable()
    {
        CancelInvoke("SpawnBomb");
        GameManagerScript.Instance.commenceBombSpawn = false;
    }
}
