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
    public Text nameHeaderEnemy;
    public GameObject speechBubbleLeft;
    public GameObject speechBubbleRight;
    public Text bubbleTextLeft;
    public Text bubbleTextRight;
    public float duration = 5f;


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

    public void ShowBubbleRight(string message, string name)
    {
        nameHeaderEnemy.text = name;
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
        float delay = 11f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("My family are starving.... We aren't the only ones. \nI have to stop baron to save the world.");
            Invoke("Boomberry", 5f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void SecondEncounter()
    {
        float delay = 11f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("Phew, i nearly lost a wing to those bombs. \ni dont think thats the last of them.");
            Invoke("Festerpaw", 5f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void ThirdEncounter()
    {
        float delay = 11f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("i know im a fruit fly and all... \n\nbut there are still limits.");
            Invoke("Peelz", 5f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void FourthEncounter()
    {
        float delay = 11f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("Poor guy, just trying to get by like the rest of us. \nBaron must be close.");
            Invoke("Logjam", 5f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void FinalEncounter()
    {
        float delay = 11f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("hey, you! im here to stop all this monkeying around. hope you're ready.");
            Invoke("Baron", 5f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void Boomberry()
    {
        Animator animator = characters[1].GetComponent<Animator>();
        SmokeEnterExit();
        characters[1].SetActive(true);
        ShowBubbleRight("*squeak* You? tryna get to the boss? \nHow about you get through me first. *squeak*", "BoomBerry");
        animator.SetTrigger("boomberryOn");
        Invoke("SmokeEnterExit", 4.8f);
        Invoke("DisableCharacter", 5f);
    }

    public void Festerpaw()
    {
        Animator animator = characters[2].GetComponent<Animator>();
        SmokeEnterExit();
        characters[2].SetActive(true);
        ShowBubbleRight("Did that sad excuse for a squirrel nearly blow you up? \nMy methods are a little more... sick", "Festerpaw");
        Invoke("SmokeEnterExit", 4.8f);
        Invoke("DisableCharacter", 5f);
    }

    public void Peelz()
    {
        Animator animator = characters[3].GetComponent<Animator>();
        SmokeEnterExit();
        characters[3].SetActive(true);
        ShowBubbleRight("I was just in it for the free food. \nthen it all started smelling funny", "Peelz");
        Invoke("SmokeEnterExit", 4.8f);
        Invoke("DisableCharacter", 5f);
    }

    public void Logjam()
    {
        Animator animator = characters[4].GetComponent<Animator>();
        SmokeEnterExit();
        characters[4].SetActive(true);
        ShowBubbleRight("Useless ape. He's all for show. Me? i dont give a dam. get it? cause im a beaver..", "Logjam");
        Invoke("SmokeEnterExit", 4.8f);
        Invoke("DisableCharacter", 5f);
    }

    public void Baron()
    {
        Animator animator = characters[0].GetComponent<Animator>();
        SmokeEnterExit();
        characters[0].SetActive(true);
        ShowBubbleRight("Those imbeciles. why must i do everything myself. \n\nthe fruit are mine!!!", "Baron Von Fruit");
        Invoke("SmokeEnterExit", 4.8f);
        Invoke("DisableCharacter", 5f);
    }

    public void SmokeEnterExit()
    {
        Animator animator = smoke.GetComponent<Animator>();
        smoke.SetActive(true);
        animator.SetTrigger("smokeOn");
        animator.SetTrigger("smokeIdle");
    }

    public void DisableCharacter()
    {
        for (int i = 0; i < characters.Length; i++)
            if (characters[i].activeSelf)
            {
                characters[i].SetActive(false);
            }
    }

}
