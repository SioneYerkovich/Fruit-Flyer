using Mono.Cecil.Cil;
using System.Threading;
using UnityEngine;

public class RottenSpawnScript : MonoBehaviour
{

    public GameObject[] rottenFruits;
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
        if (!GameManagerScript.Instance.commenceRottenSpawn && GameManagerScript.Instance.gameStarted)
        {
            GameManagerScript.Instance.commenceRottenSpawn = true;
            InvokeRepeating("SpawnRottenFruit", 1f, Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
    GameObject GetRandomObject()
    {
        int index = Random.Range(0, rottenFruits.Length);
        return rottenFruits[index];
    }

    Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0f);
    }

    void SpawnRottenFruit()
    {
        if (GameManagerScript.Instance.gameStarted)
        {
            Debug.Log("rotten spawned");
            GameObject chosenFruit = GetRandomObject();
            Instantiate(chosenFruit, GetRandomPosition(), Quaternion.identity);
            CancelInvoke("SpawnRottenFruit");
            InvokeRepeating("SpawnRottenFruit", Random.Range(minSpawnTime, maxSpawnTime), Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    private void OnDisable()
    {
        CancelInvoke("SpawnRottenFruit");
        GameManagerScript.Instance.commenceRottenSpawn = false;
    }


}