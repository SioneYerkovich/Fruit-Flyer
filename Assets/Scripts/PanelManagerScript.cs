using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PanelManagerScript : MonoBehaviour
{
    public GameObject player;
    InputAction jumpAction;
    public GameObject[] panels;
    public Text timerText;
    public GameObject introText;
    private void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {

        if (GameManagerScript.Instance.gameIntro)
        {
            if (jumpAction.IsPressed())
            {
                StartIntroAnimation();
                GameManagerScript.Instance.gameIntro = false;
            }
        }
        ToggleSpeedBoostPanel();
    }

    public void StartGameAnimation()
    {
        Animator animator = panels[0].GetComponent<Animator>();
        animator.SetTrigger("StartGame");
    }

    public void StartIntroAnimation()
    {
        Animator animator = panels[4].GetComponent<Animator>();
        animator.SetTrigger("fadeIntro");
    }

    public void StartGame()
    {
        player.SetActive(true);
        panels[0].SetActive(false);
        panels[1].SetActive(true);
    }

    public void TogglePauseMenu()
    {
        if (panels[2].activeSelf == false)
        {
            Time.timeScale = 0;
            panels[2].SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            panels[2].SetActive(false);
        }
    }

    public void ToggleSpeedBoostPanel()
    {
        if (FruitManagerScript.Instance.isBoosted)
        {
            panels[3].SetActive(true);
            float timeRemaining = FruitManagerScript.Instance.speedBoostTimer;

            if (timeRemaining > 0)
            {
                timerText.text = Mathf.Ceil(timeRemaining).ToString() + " s";
            }
        }
        else
        {
            panels[3].SetActive(false);
        }
    }


    public void ExitGameAnimation()
    {
        ShowStartMenu();
        Animator animator = panels[0].GetComponent<Animator>();
        animator.Play("fade_in_start_menu");
    }

    public void ShowStartMenu()
    {
        panels[0].SetActive(true);
        panels[1].SetActive(false);
        panels[2].SetActive(false);
        panels[3].SetActive(false);
    } 

    public void DisableIntro()
    {
        panels[4].SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
