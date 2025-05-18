using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public static GameManagerScript Instance;
    public bool commenceFruitSpawn = false;
    public bool commenceBombSpawn = false;
    public bool commencePowerupSpawn = false;
    public bool characterMonologue = true;
    public bool gameStarted = false;
    public bool gameIntro = true;
    public bool activateSpeech = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        Instance = this;
    }


}
