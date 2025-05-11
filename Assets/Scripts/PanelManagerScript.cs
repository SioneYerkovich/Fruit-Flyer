using JetBrains.Annotations;
using UnityEngine;

public class PanelManagerScript : MonoBehaviour
{
    public GameObject[] panels;

    private void Start()
    {

    }

    public void StartGameAnimation()
    {
        Animator animator = panels[0].GetComponent<Animator>();
        animator.SetTrigger("StartGame");
    }

    public void StartGame()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);
    }

    public void TogglePauseMenu()
    {
        if (panels[2].activeSelf == false)
        {
            panels[2].SetActive(true);
        }
        else
        {
            panels[2].SetActive(false);
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
        panels[2].SetActive(false);
        panels[1].SetActive(false);
        panels[0].SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
