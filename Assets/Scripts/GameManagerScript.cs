using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    Vector3 checkpointPosition;
    public static GameManagerScript Instance;
    public bool characterMonologue = true;
    public bool gameStarted = false;
    public bool gameIntro = true;
    public bool activateSpeech = false;
    public int currentCheckpoint = 0;
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

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;

        PlayerPrefs.SetFloat("CheckpointX", position.x);
        PlayerPrefs.SetFloat("CheckpointY", position.y);
        PlayerPrefs.SetFloat("CheckpointZ", position.z);
        PlayerPrefs.Save();
    }

    public void SaveCheckpoint()
    {
        PlayerPrefs.SetInt("Checkpoint", currentCheckpoint);
        PlayerPrefs.Save();
    }

    public void LoadCheckpoint(GameObject player)
    {
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            int currentStage = PlayerPrefs.GetInt("StageNumber", 1);
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            float z = PlayerPrefs.GetFloat("CheckpointZ");
            player.transform.position = new Vector3(x, y, z);

            for (int i = 0; i < StageManagerScript.Instance.stages.Length; i++)
                StageManagerScript.Instance.stages[i].SetActive(i == currentStage);
        }
    }
}
