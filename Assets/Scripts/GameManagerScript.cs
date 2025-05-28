using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManagerScript : MonoBehaviour
{
    public GameObject[] Mechanic;
    public GameObject player;
    public static GameManagerScript Instance;
    public bool finalBomb = false;
    public bool gameCompleted = false;
    public bool commenceFruitSpawn = false;
    public bool commenceBombSpawn = false;
    public bool commencePowerupSpawn = false;
    public bool commenceRottenSpawn = false;
    public bool commenceWallSpawn = false;
    public bool characterMonologue = true;
    public bool gameStarted = false;
    public bool gameIntro = true;
    public bool gameOutro = false;
    public bool activateSpeech = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ToggleMechanic();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleMechanic()
    {
        int currentStage;
        currentStage = PlayerPrefs.GetInt("CurrentStage", 0);
        switch (currentStage)
        {
            case 0:
                Mechanic[0].SetActive(true);
                Mechanic[1].SetActive(false);
                Mechanic[2].SetActive(false);
                Mechanic[3].SetActive(false);
                break;
            case 1:
                Mechanic[0].SetActive(true);
                Mechanic[1].SetActive(true);
                break;
            case 2:
                Mechanic[0].SetActive(false);
                Mechanic[1].SetActive(true);
                Mechanic[2].SetActive(true);
                break;
            case 3:
                Mechanic[1].SetActive(false);
                Mechanic[0].SetActive(true);
                Mechanic[2].SetActive(true);
                Mechanic[3].SetActive(true);
                break;
            case 4:
                Mechanic[0].SetActive(true);
                Mechanic[1].SetActive(true);
                Mechanic[2].SetActive(true);
                Mechanic[3].SetActive(true);
                break;
            default:
                Mechanic[0].SetActive(true);
                break;
        }
    }
}
