using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SpeechScript : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject smoke;
    public GameObject speechBubbleLeft;
    public GameObject speechBubbleRight;
    InputAction jumpAction;
    public Text bubbleTextLeft;
    public Text bubbleTextRight;
    public float duration = 8f;


    public void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        speechBubbleRight.SetActive(false);
    }
    public void Update()
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
        jumpAction.Enable();
    }

    public void FirstEncounter()
    {
        float delay = 12f;

        if (GameManagerScript.Instance.characterMonologue && !GameManagerScript.Instance.gameStarted)
        {
            jumpAction.Disable();
            ShowBubbleLeft("My family are starving.... theres no way we're alone. \r\nI have to stop baron to save my family.");
            Invoke("Boomberry", 6f);
            Invoke("monologueCompleted", delay);
        }
    }

    public void Boomberry()
    {
        Animator animator = characters[1].GetComponent<Animator>();
        SmokeEnterExit();
        characters[1].SetActive(true);
        ShowBubbleRight("You? tryna get to the boss? \r\nHow about you get through me first teehee");
        animator.SetTrigger("boomberryOn");
        Invoke("SmokeEnterExit", 5.9f);
        Invoke("DisableBoomberry", 6f);
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
