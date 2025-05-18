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

    public void ActivateBoost(float duration)
    {
        boostedSpeed = boostedMoveSpeed;
        speedBoostTimer = duration;
        isBoosted = true;
    }

    public void EndBoost()
    {
        isBoosted = false;
        speedBoostTimer = 0f;
    }

    public void BoostControl()
    {
        if (isBoosted)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0f)
            {
                EndBoost();
            }
        }
    }

}
