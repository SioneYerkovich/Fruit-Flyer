using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PanelManagerScript : MonoBehaviour
{
    public static PanelManagerScript Instance;
    public GameObject player;
    public GameObject[] panels;
    public Text speedTimerText;
    public Text bonusTimerText;
    public GameObject introText;
    InputAction jumpAction;

    private void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        CharacterMonologue();

        if (GameManagerScript.Instance.gameIntro)
        {
            if (jumpAction.IsPressed())
            {
                StartIntroAnimation();
                GameManagerScript.Instance.gameIntro = false;
            }
        }

        if (GameManagerScript.Instance.gameOutro)
        {
            jumpAction.Enable();
            if (jumpAction.IsPressed())
            {
                ShowStartMenu();
                PlayerPrefs.DeleteKey("CurrentStage");
            }
        }

        TogglePowerupPanel();
    }

    public void Awake()
    {
        Instance = this;
    }

    public void StartGameAnimation()
    {
        LoadCheckpoint(player);
        player.SetActive(true);
        Animator animator = panels[0].GetComponent<Animator>();
        animator.SetTrigger("StartGame");
        Invoke("StartGame", 1f);
    }


    public void StartIntroAnimation()
    {
        ShowStartMenu();
        Animator animator = panels[4].GetComponent<Animator>();
        animator.SetTrigger("fadeIntro");
        Invoke("DisableIntro", 2f);
    }

    public void StartGame()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);
        GameManagerScript.Instance.activateSpeech = true;
    }

    //Method to control pause logic
    public void TogglePauseMenu()
    {
        if (panels[2].activeSelf == false) //Onclick, if the pause menu is not active
        {
            Time.timeScale = 0; //Freeze the game state
            panels[2].SetActive(true); //Set the pause menu as active
        }
        else //This is handled by the resume button (in the pause menu)
        {
            Time.timeScale = 1; //Unfreeze the game
            panels[2].SetActive(false); //Deactivate the pause menu
        }
    }

    //Method to control the Powerup Panel
    public void TogglePowerupPanel()
    {
        if (SpeedBoostManagerScript.Instance.isBoosted) //Checks for speed boosts
        {
            panels[3].SetActive(true); //activates the powerup panel
            float timeRemaining = SpeedBoostManagerScript.Instance.speedBoostTimer; //Gets the current value of speedBoostTimer

            if (timeRemaining > 0) //checks the timer hasn't hit 0 seconds
            {
                speedTimerText.text = Mathf.Ceil(timeRemaining).ToString() + " s"; //rounds the number to the nearest whole
                                                                                   //and adds an "s" at the end
            }
        }
        else //if no speed boost, deactivate
        {
            panels[3].SetActive(false);
        }

        if (DoublePointManagerScript.Instance.bonus) //Checks for double point bonus
        {
            panels[7].SetActive(true);
            float timeRemaining = DoublePointManagerScript.Instance.bonusTimer;

            if (timeRemaining > 0) 
            {
                bonusTimerText.text = Mathf.Ceil(timeRemaining).ToString() + " s";
            }
        }
        else
        {
            panels[7].SetActive(false);
        }
    }

    public void ShowStartMenu()
    {
        panels[0].SetActive(true);
        panels[1].SetActive(false);
        panels[2].SetActive(false);
        panels[9].SetActive(false);
    } 

    public void DisableIntro()
    {
        panels[4].SetActive(false);
    }

    public void CharacterMonologue()
    {
        if (GameManagerScript.Instance.characterMonologue && GameManagerScript.Instance.activateSpeech)
        {
            panels[5].SetActive(true);
        }
        else
        {
            panels[5].SetActive(false);
        }
    }

    public void StageComplete()
    {
        Animator animator = panels[6].GetComponent<Animator>();
        panels[6].SetActive(true);
        animator.Play("fade_out");
        Invoke("StageTransitionDelay", 2f);
    }

    public void StageTransitionDelay()
    {
        Animator animator = panels[6].GetComponent<Animator>();
        animator.SetTrigger("stageCompleted");
        Invoke("DeactivateFadePanel", 2f);
    }

    public void DeactivateFadePanel()
    {
        panels[6].SetActive(false);
        ObjectiveManager.Instance.stageComplete = false;
    }

    public void LoadCheckpoint(GameObject player)
    {
        if (PlayerPrefs.HasKey("CurrentStage"))
        {
            int currentStage = PlayerPrefs.GetInt("CurrentStage", 0);
            Vector3 spawnPoint = StageManagerScript.Instance.GetStartPosition(currentStage);
            player.transform.position = spawnPoint;

            for (int i = 0; i < StageManagerScript.Instance.stages.Length; i++)
                StageManagerScript.Instance.stages[i].SetActive(i == currentStage);
        }
        else
        {
            StageManagerScript.Instance.stages[0].SetActive(true);
        }
    }

    public void DeathPanel()
    {
        panels[8].SetActive(true);
    }

    public void RestartGame()
    {
        panels[8].SetActive(false);
        StageComplete();
        FergusScript.instance.ResetPlayer();
        ObjectiveManager.Instance.ResetScene();
    }

    public void CallOutro()
    {
        Animator animator = panels[9].GetComponent<Animator>();
        panels[9].SetActive(true);
        animator.SetTrigger("triggerOutro");
        GameManagerScript.Instance.gameOutro = true;

    }

    //Method to close the game
    public void CloseGame()
    {
        Application.Quit(); //Closes the application
    }

}
