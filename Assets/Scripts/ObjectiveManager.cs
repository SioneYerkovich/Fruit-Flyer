using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    public AudioSource winSound;
    public GameObject player;
    public int fruitsCollected = 0;
    public int fruitsNeeded = 100;
    public Text scoreText;
    public bool stageComplete = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winSound = GetComponent<AudioSource>();
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
        //Double points active event handler
        if (DoublePointManagerScript.Instance.bonus)
        {
            fruitsCollected += amount * 2; //Double the value added to the score text
            scoreText.text = fruitsCollected.ToString() + "/100";
        }
        else //if double points not active, do the standard method of 1 to 1 scoring
        {
            fruitsCollected += amount;
            scoreText.text = fruitsCollected.ToString() + "/100";
        }

        //Objective reached event handler
        if (fruitsCollected >= fruitsNeeded) 
        {
            if (PlayerPrefs.GetInt("CurrentStage", 0) == 4) //if player is currently on the final boss
            //handle it this way (AKA trigger gameCompleted boolean for different logic)
            {
                GameManagerScript.Instance.gameCompleted = true;
                StageManagerScript.Instance.GoToNextStage();
                winSound.Play();
                stageComplete = true;
                FergusScript.instance.ResetPlayer();
                ResetScene();
            }
            else //Otherwise just trigger a regular next stage event
            {
                StageManagerScript.Instance.GoToNextStage();
                winSound.Play();
                stageComplete = true;
                FergusScript.instance.ResetPlayer();
                ResetScene();
            }

        }
    }

    public void ResetScene()
    {
            fruitsCollected = 0;
            scoreText.text = fruitsCollected.ToString() + "/100";
            SpeedBoostManagerScript.Instance.EndBoost();
            DoublePointManagerScript.Instance.EndBonus();
            GameManagerScript.Instance.characterMonologue = true;
            GameManagerScript.Instance.gameStarted = false;

            GameObject[] spawnedFruits = GameObject.FindGameObjectsWithTag("Fruit");
            GameObject[] spawnedBombs = GameObject.FindGameObjectsWithTag("Bomb");
            GameObject[] spawnedSpeed = GameObject.FindGameObjectsWithTag("SpeedBoost");
            GameObject[] spawnedBonus = GameObject.FindGameObjectsWithTag("DoublePoints");
            GameObject[] spawnedRotten = GameObject.FindGameObjectsWithTag("Rotten");
            GameObject[] spawnedWall = GameObject.FindGameObjectsWithTag("Obstacle");

            List<GameObject> allObjects = new List<GameObject>();
            allObjects.AddRange(spawnedFruits);
            allObjects.AddRange(spawnedBombs);
            allObjects.AddRange(spawnedSpeed);
            allObjects.AddRange(spawnedBonus);
            allObjects.AddRange(spawnedRotten);
            allObjects.AddRange(spawnedWall);
            GameObject[] spawnedStuff = allObjects.ToArray();

            foreach (GameObject objects in spawnedStuff)
            {
                Destroy(objects);
            }
    }

    public void FullGameReset()
    {
        PlayerPrefs.DeleteKey("CurrentStage");
        PanelManagerScript.Instance.CloseGame();
    }

}
