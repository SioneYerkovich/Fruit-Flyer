using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    public GameObject player;
    public int fruitsCollected = 0;
    public int fruitsNeeded = 100;
    public Text scoreText;
    public bool stageComplete = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    //Update is called once per frame
    void Update()
    {

    }

    public void Awake()
    {
        Instance = this;
    }

    public void AddPoints(int amount)
    {
        fruitsCollected += amount;
        scoreText.text = fruitsCollected.ToString() + "/100";
        if (fruitsCollected >= fruitsNeeded)
        {
            stageComplete = true;
            StageManagerScript.Instance.GoToNextStage();
            ResetScene();
        }
    }

    public void ResetScene()
    {
        if (stageComplete == true)
        {
            fruitsCollected = 0;
            scoreText.text = fruitsCollected.ToString() + "/100";
            SpeedBoostManagerScript.Instance.EndBoost();
            GameManagerScript.Instance.characterMonologue = true;
            GameManagerScript.Instance.gameStarted = false;

            GameObject[] spawnedFruits = GameObject.FindGameObjectsWithTag("Fruit");
            GameObject[] spawnedBombs = GameObject.FindGameObjectsWithTag("Bomb");
            GameObject[] spawnedPowerups = GameObject.FindGameObjectsWithTag("Powerup");
            GameObject[] spawnedRotten = GameObject.FindGameObjectsWithTag("Rotten");

            List<GameObject> allObjects = new List<GameObject>();
            allObjects.AddRange(spawnedFruits);
            allObjects.AddRange(spawnedBombs);
            allObjects.AddRange(spawnedPowerups);
            allObjects.AddRange(spawnedRotten);
            GameObject[] spawnedStuff = allObjects.ToArray();

            foreach (GameObject objects in spawnedStuff)
            {
                Destroy(objects);
            }
        }
    }

}
