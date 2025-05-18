using Mono.Cecil.Cil;
using System.Threading;
using UnityEngine;

public class FruitSpawnScript : MonoBehaviour
{

    public GameObject[] Fruits;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;
    public BoxCollider2D spawnArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerScript.Instance.commenceFruitSpawn && GameManagerScript.Instance.gameStarted)
        {
            GameManagerScript.Instance.commenceFruitSpawn = true;
            InvokeRepeating("SpawnFruit", 1f, Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
    GameObject GetRandomObject()
    {
        int index = Random.Range(0, Fruits.Length);
        return Fruits[index];
    }

    Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0f);
    }

    void SpawnFruit()
    {
        if (GameManagerScript.Instance.gameStarted)
        {
            GameObject chosenFruit = GetRandomObject();
            Instantiate(chosenFruit, GetRandomPosition(), Quaternion.identity);
            CancelInvoke("SpawnFruit");
            InvokeRepeating("SpawnFruit", Random.Range(minSpawnTime, maxSpawnTime), Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

}