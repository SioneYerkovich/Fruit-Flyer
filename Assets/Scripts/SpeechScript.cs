using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SpeechScript : MonoBehaviour
{
    private int currentStage;
    public GameObject[] characters;
    public GameObject smoke;
    public GameObject speechBubbleLeft;
    public GameObject speechBubbleRight;
    public Text bubbleTextLeft;
    public Text bubbleTextRight;
    public float duration = 4f;


    public void Start()
    {
        speechBubbleRight.SetActive(false);
    }
    public void Update()
    {

    }

    public void Awake()
    {

    }
    public void ShowBubbleLeft(string message)
    {
        speechBubbleLeft.SetActive(true);
        bubbleTextLeft.text = message;
        Invoke("HideBubbleLeft", duration);
    }

    public void ShowBubbleRight(string message)
    {
        speechBubbleRight.SetActive(true);
        bubbleTextRight.text = message;
        Invoke("HideBubbleRight", duration);
    }

    public void HideBubbleLeft()
    {
        speechBubbleLeft.SetActive(false);
    }
    public void HideBubbleRight()
    {
        speechBubbleRight.SetActive(false);
    }

    public void monologueCompleted()
    {
        GameManagerScript.Instance.characterMonologue = false;
    }

    public void EncounterSwitch()
    {
            currentStage = PlayerPrefs.GetInt("CurrentStage", 0);
            switch (currentStage)
            {
                case 0:
                    FirstEncounter();
                    Debug.Log("first encounter called");
                    break;
                case 1:
                    SecondEncounter();
                    Debug.Log("second encounter called");
                    break;
                case 2:
                    ThirdEncounter();
                    Debug.Log("third encounter called");
                    break;
                case 3:
                    FourthEncounter();
                    Debug.Log("fourth encounter called");
                    break;
                case 4:
                    FinalEncounter();
                    Debug.Log("final encounter called");
                    break;
            }
    }

    public void FirstEncounter()
    {
        float delay = 9f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("My family are starving.... theres no way we're alone. \nI have to stop baron to save my family.");
            Invoke("Boomberry", 5f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void SecondEncounter()
    {
        float delay = 9f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("Phew, i nearly lost a wing to those bombs. \n but i dont think thats the last time i'll see them.");
            Invoke("Festerpaw", 5f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void ThirdEncounter()
    {
        float delay = 9f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("My family are starving.... theres no way we're alone. \nI have to stop baron to save my family.");
            Invoke("Boomberry", 5f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void FourthEncounter()
    {
        float delay = 9f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("My family are starving.... theres no way we're alone. \nI have to stop baron to save my family.");
            Invoke("Boomberry", 5f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void FinalEncounter()
    {
        float delay = 9f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("My family are starving.... theres no way we're alone. \nI have to stop baron to save my family.");
            Invoke("Boomberry", 5f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void Boomberry()
    {
        Animator animator = characters[1].GetComponent<Animator>();
        SmokeEnterExit();
        characters[1].SetActive(true);
        ShowBubbleRight("You? tryna get to the boss? \nHow about you get through me first teehee");
        animator.SetTrigger("boomberryOn");
        Invoke("SmokeEnterExit", 3.8f);
        Invoke("DisableBoomberry", 4f);
    }

    public void Festerpaw()
    {
        Animator animator = characters[1].GetComponent<Animator>();
        SmokeEnterExit();
        characters[1].SetActive(true);
        ShowBubbleRight("Did that sad excuse for a squirrel nearly blow you up? \nMy methods are a little more... sick");
        animator.SetTrigger("boomberryOn");
        Invoke("SmokeEnterExit", 3.8f);
        Invoke("DisableBoomberry", 4f);
    }

    public void SmokeEnterExit()
    {
        Animator animator = smoke.GetComponent<Animator>();
        smoke.SetActive(true);
        animator.SetTrigger("smokeOn");
        animator.SetTrigger("smokeIdle");
    }

    public void DisableBoomberry()
    {
        characters[1].SetActive(false);
    }

}
