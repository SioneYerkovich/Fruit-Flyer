using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    public int fruitsCollected = 0;
    public int fruitsNeeded = 100;
    public Text scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
            //Trigger next stage
        }
    }

}
