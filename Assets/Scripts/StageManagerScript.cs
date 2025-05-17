using UnityEngine;

public class StageManagerScript : MonoBehaviour
{
    public static StageManagerScript Instance;
    public GameObject[] stages;
    private int currentStage = 0;
    public float scrollSpeed = 0.1f;
    private float width;

    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        BackgroundMovement();
    }

    public void Awake()
    {
        Instance = this;
    }

    public void GoToNextStage()
    {
        if (currentStage < stages.Length - 1)
        {
            stages[currentStage].SetActive(false);
            currentStage++;
            stages[currentStage].SetActive(true);

           GameManagerScript.Instance.SetCheckpoint(GetStageStart(currentStage));
        }
    }

    Vector3 GetStageStart(int stageIndex)
    {
        return stages[stageIndex].transform.Find("StartPoint").position;
    }

    public void BackgroundMovement()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x < -width)
        {
            transform.position += new Vector3(width * 2f, 0, 0);
        }
    }
}
