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
        //Once true perform the execution code
        if (!GameManagerScript.Instance.commenceFruitSpawn && GameManagerScript.Instance.gameStarted)
        {
            GameManagerScript.Instance.commenceFruitSpawn = true; //becomes true to stop this code from repeating, it is just for the initial spawn and continue spawning from the method SpawnFruit()
            InvokeRepeating("SpawnFruit", 1f, Random.Range(minSpawnTime, maxSpawnTime)); //This will only execute once due to the boolean
        }
    }

    //Method to select a random fruit from the array
    GameObject GetRandomObject()
    {
        int index = Random.Range(0, Fruits.Length); //select a random fruit from the range passed into the function (from 0 to the length of the array)
        return Fruits[index]; //return the index chosen
    }

    //Creates a random position for the fruit, defined by the gameObjects collider size or "bounds"
    Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0f);
    }

    //Method to control fruit spawn
    void SpawnFruit()
    {
        if (GameManagerScript.Instance.gameStarted) //if the player has performed the startText prompt
        {
            GameObject chosenFruit = GetRandomObject(); //Get the random fruit
            Instantiate(chosenFruit, GetRandomPosition(), Quaternion.identity); //Create an instance of it and attach the random position to it
            CancelInvoke("SpawnFruit"); //Cancel the initial invoke in Update() OR the invoke below (depends which is active)
            InvokeRepeating("SpawnFruit", Random.Range(minSpawnTime, maxSpawnTime), Random.Range(minSpawnTime, maxSpawnTime)); //Restart the invoke with a brand new fruit and position
        }
    }

}