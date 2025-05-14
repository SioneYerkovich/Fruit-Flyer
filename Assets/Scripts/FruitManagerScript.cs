using System.Collections;
using UnityEngine;

public class FruitManagerScript : MonoBehaviour
{
    public static FruitManagerScript Instance;
    public float normalSpeed = 5;
    public float deadZone = -9;
    public float boostedSpeed;
    public float boostedMoveSpeed = 7;
    public float speedBoostTimer;
    public bool isBoosted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isBoosted)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <=0f)
            {
                EndBoost();
            }
        }
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

    private void EndBoost()
    {
        isBoosted = false;
        speedBoostTimer = 0f;
    }

}
