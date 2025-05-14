using Mono.Cecil.Cil;
using System.Threading;
using UnityEngine;

public class PowerupSpawnScript : MonoBehaviour
{
    private bool commenceSpawn = false;
    public GameObject[] Powerups;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;
    public BoxCollider2D spawnArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!commenceSpawn && GameManagerScript.Instance.gameStarted)
        {
            commenceSpawn = true;
            InvokeRepeating("SpawnPowerup", 0f, Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
    GameObject GetRandomObject()
    {
        int index = Random.Range(0, Powerups.Length);
        return Powerups[index];
    }

    Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0f);
    }

    void SpawnPowerup()
    {
        if (GameManagerScript.Instance.gameStarted)
        {
            GameObject chosenPowerup = GetRandomObject();
            Instantiate(chosenPowerup, GetRandomPosition(), Quaternion.identity);
            CancelInvoke("SpawnPowerup");
            InvokeRepeating("SpawnPowerup", Random.Range(minSpawnTime, maxSpawnTime), Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
}