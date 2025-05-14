using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;
    public bool gameStarted = false;
    public bool gameIntro = true;
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
