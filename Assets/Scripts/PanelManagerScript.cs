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
    public Text timerText;
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

        ToggleSpeedBoostPanel();
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
        if (SpeedBoostManagerScript.Instance.isBoosted)
        {
            panels[3].SetActive(true);
            float timeRemaining = SpeedBoostManagerScript.Instance.speedBoostTimer;

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
        if (ObjectiveManager.Instance.stageComplete == true)
        {
            Animator animator = panels[6].GetComponent<Animator>();
            panels[6].SetActive(true);
            animator.Play("fade_out");
            Invoke("StageTransitionDelay", 2f);
        }
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

    public void CloseGame()
    {
        Application.Quit();
    }

}
