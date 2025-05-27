using Unity.Loading;
using UnityEngine;

public class StageManagerScript : MonoBehaviour
{
    public static StageManagerScript Instance;
    public GameObject[] stages;
    private int currentStage;

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

    //Method to handle the stage transition
    public void GoToNextStage()
    {
        if (currentStage < stages.Length -1) //Checks if theyre not on the final stage
        {
            PanelManagerScript.Instance.StageComplete();
            Invoke("SaveCheckpoint", 2f); //Delay SaveCheckpoint to allow the background to fade
                                          //this will hide the stage swap logic
        }
        else //if theyre on the final stage dont save a checkpoint
        {
            PanelManagerScript.Instance.StageComplete();
        }
    }

    //Method to save checkpoint
    public void SaveCheckpoint()
    {
        currentStage = PlayerPrefs.GetInt("CurrentStage", 0); //Retrieve the current player stage in Player prefs

        if (currentStage < stages.Length - 1) //if theyre not on the last stage
        {
            //stages[] is the array declared above and assigned in the inspector
            stages[currentStage].SetActive(false); //deactivate the current stage using the int value of player prefs
            currentStage++; //Increment player prefs to handle the stage management
            stages[currentStage].SetActive(true); //use the new increment value and set that stage int as active
            PlayerPrefs.SetInt("CurrentStage", currentStage);
        }
    }

    public Vector3 GetStartPosition(int stageIndex)
    {
        return stages[stageIndex].transform.Find("StartPoint").position;
    }


}
