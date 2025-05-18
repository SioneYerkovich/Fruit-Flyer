using UnityEngine;

public class StageManagerScript : MonoBehaviour
{
    public static StageManagerScript Instance;
    public GameObject[] stages;
    private int currentStage = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Awake()
    {
        Instance = this;
    }

    public void GoToNextStage()
    {
        if (currentStage < stages.Length - 1)
        {
            PanelManagerScript.Instance.StageComplete();
            Invoke("SaveCheckpoint", 2f);
        }
    }

    public void SaveCheckpoint()
    {
        stages[currentStage].SetActive(false);
        currentStage++;
        stages[currentStage].SetActive(true);
        PlayerPrefs.SetInt("CurrentStage", currentStage);
    }

    public Vector3 GetStartPosition(int stageIndex)
    {
        return stages[stageIndex].transform.Find("StartPoint").position;
    }


}
