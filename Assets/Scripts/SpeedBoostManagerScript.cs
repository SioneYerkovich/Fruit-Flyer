using UnityEngine;

public class SpeedBoostManagerScript : MonoBehaviour
{
    public static SpeedBoostManagerScript Instance;
    public float normalSpeed = 5;
    public float boostedSpeed;
    public float boostedMoveSpeed = 10;
    public float speedBoostTimer;
    public bool isBoosted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        BoostControl();
    }

    private void Awake()
    {
        Instance = this;
    }

    //Method to apply variable changes when boost activates
    public void ActivateBoost(float duration)
    {
        boostedSpeed = boostedMoveSpeed;
        speedBoostTimer = duration;
        isBoosted = true; //Triggers isBoosted boolean
    }

    //Ends the boost period and resets it
    public void EndBoost()
    {
        isBoosted = false; //resets isBoosted to turn toggle the boost control
        speedBoostTimer = 0f; //reset the boost timer
    }

    //Method that controls the boost activation
    public void BoostControl()
    {
        if (isBoosted) //triggered by ActivateBoost()
        {
            speedBoostTimer -= Time.deltaTime; //assigns a incementally decreasing value to the timer
            if (speedBoostTimer <= 0f) //if the timer reaches 0
            {
                EndBoost(); //call the EndBoost method to deactivate boost
            }
        }
    }

}
