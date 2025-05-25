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
    float durationFergus = 5f;
    float durationEnemy = 6f;


    public void Start()
    {
        speechBubbleRight.SetActive(false);
    }
    public void Update()
    {

    }

    public void ShowBubbleLeft(string message)
    {
        speechBubbleLeft.SetActive(true);
        bubbleTextLeft.text = message;
        Invoke("HideBubbleLeft", durationFergus);
    }

    public void ShowBubbleRight(string message, string name)
    {
        nameHeaderEnemy.text = name;
        speechBubbleRight.SetActive(true);
        bubbleTextRight.text = message;
        Invoke("HideBubbleRight", durationEnemy);
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
                    break;
                case 1:
                    SecondEncounter();
                    break;
                case 2:
                    ThirdEncounter();
                    break;
                case 3:
                    FourthEncounter();
                    break;
                case 4:
                    if (GameManagerScript.Instance.gameCompleted == false)
                    {
                        FinalEncounter();
                    }
                    else
                    {
                        Ending();
                    }
                    break;
                    
            }
    }

    public void FirstEncounter()
    {
        float delay = 13f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("My family are starving.... We aren't the only ones. \nI have to stop baron to save the world.");
            Invoke("Boomberry", 6f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void SecondEncounter()
    {
        float delay = 13f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("Phew, i nearly lost a wing to those bombs. \ni dont think thats the last of them.");
            Invoke("Festerpaw", 6f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void ThirdEncounter()
    {
        float delay = 13f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("i know im a fruit fly and all... \n\nbut there are still limits.");
            Invoke("Peelz", 6f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void FourthEncounter()
    {
        float delay = 13f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("Poor guy, just trying to get by like the rest of us. \nBaron must be close.");
            Invoke("Logjam", 6f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void FinalEncounter()
    {
        float delay = 13f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("hey, you! im here to stop all this monkeying around. hope you're ready.");
            Invoke("Baron", 6f);
            Invoke("monologueCompleted", delay);
        }
    }
    public void Ending()
    {
        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            ShowBubbleLeft("Enough. Theres no escaping the inevitable, Baron.");
            Invoke("BaronDeath", 6f);
        }
    }

    public void Boomberry()
    {
        Animator animator = characters[1].GetComponent<Animator>();
        SmokeEnterExit();
        characters[1].SetActive(true);
        ShowBubbleRight("*squeak* You? tryna get to the boss? \nHow about you get through me first. *squeak*", "BoomBerry");
        animator.SetTrigger("boomberryOn");
        Invoke("SmokeEnterExit", 5.8f);
        Invoke("DisableCharacter", 6f);
    }

    public void Festerpaw()
    {
        Animator animator = characters[2].GetComponent<Animator>();
        SmokeEnterExit();
        characters[2].SetActive(true);
        ShowBubbleRight("Did that sad excuse for a squirrel nearly blow you up? \nMy methods are a little more... sickly", "Festerpaw");
        Invoke("SmokeEnterExit", 5.8f);
        Invoke("DisableCharacter", 6f);
    }

    public void Peelz()
    {
        Animator animator = characters[3].GetComponent<Animator>();
        SmokeEnterExit();
        characters[3].SetActive(true);
        ShowBubbleRight("I was just in it for the free food. \nthen it all started smelling funny. You can have my share", "Peelz");
        Invoke("SmokeEnterExit", 5.8f);
        Invoke("DisableCharacter", 6f);
    }

    public void Logjam()
    {
        Animator animator = characters[4].GetComponent<Animator>();
        SmokeEnterExit();
        characters[4].SetActive(true);
        ShowBubbleRight("Useless ape. He's all for show.\nMe? i dont give a dam. get it? cause im a beaver..", "Logjam");
        Invoke("SmokeEnterExit", 5.8f);
        Invoke("DisableCharacter", 6f);
    }

    public void Baron()
    {
        Animator animator = characters[0].GetComponent<Animator>();
        SmokeEnterExit();
        characters[0].SetActive(true);
        ShowBubbleRight("Those imbeciles. why must i do everything myself. \n\nthe fruit are mine!!!", "Baron Von Fruit");
        Invoke("SmokeEnterExit", 5.8f);
        Invoke("DisableCharacter", 6f);
    }

    public void BaronDeath()
    {
        Animator animator = characters[0].GetComponent<Animator>();
        SmokeEnterExit();
        characters[0].SetActive(true);
        ShowBubbleRight("How dare you! \nif you think for one second that im going to...", "Baron Von Dust");
        Invoke("CallFinalBomb", 5f);
        Invoke("DisableCharacter", 5.6f);
        PanelManagerScript.Instance.Invoke("CallOutro", 12f);
    }

    public void SmokeEnterExit()
    {
        Animator animator = smoke.GetComponent<Animator>();
        smoke.SetActive(true);
        animator.SetTrigger("smokeOn");
        animator.SetTrigger("smokeIdle");
    }

    public void CallFinalBomb()
    {
        GameManagerScript.Instance.finalBomb = true;
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
