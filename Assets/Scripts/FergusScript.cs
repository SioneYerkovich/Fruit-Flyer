using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class FergusScript : MonoBehaviour
{
    public static FergusScript instance;
    public GameObject player;
    public GameObject startText;
    public GameObject introText;
    InputAction jumpAction;
    public Rigidbody2D FergusRigidbody;
    public Animator animator;
    public float flapStrength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.SetActive(false);
        //Retrieve the Jump input button assignment
        jumpAction = InputSystem.actions.FindAction("Jump");
        //Freeze Fergus from the start to prevent actions
        FergusRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        //This turns on startText (from Gameplay panel), and turns off once the prompt is completed
        startText.SetActive(!GameManagerScript.Instance.gameStarted && !GameManagerScript.Instance.characterMonologue);

        if (!GameManagerScript.Instance.gameStarted && !GameManagerScript.Instance.characterMonologue)
        {
            if (jumpAction.IsPressed())
            {
                GameManagerScript.Instance.gameStarted = true; //turning off the prompt
                FergusRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        //This is the movement code
        if (GameManagerScript.Instance.gameStarted) //disables the action until gameStarted = true
        {
            if (jumpAction.IsPressed()) //Code below activates when the jumpAction key assignment is pressed
            {
                //This updates the linear velocity, the 1 in Vector2 represents the upward direction as it is positive
                //0 represents the x axis and 1 represents the y axis. flapStrength controls how high
                FergusRigidbody.linearVelocity = new Vector2(0, 1) * flapStrength;
                animator.SetBool("jumping", true); //Play the jump animation
            }
            else
            {
                animator.SetBool("jumping", false); //When they release the jumpkey, set animation back to idle
            }
        }
    }

    public void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            Collider2D collider = player.GetComponent<Collider2D>();
            collider.enabled = false;
            DisableControls();
            animator.SetBool("Death", true);
            Invoke("DelayedDeath", 2.3f);
        }
        else if (other.CompareTag("Obstacle"))
        {
            player.SetActive(false);
            Time.timeScale = 0;
            PanelManagerScript.Instance.DeathPanel();
        }
    }

    public void ResetPlayer()
    {
        Collider2D collider = player.GetComponent<Collider2D>();
        collider.enabled = true;
        Time.timeScale = 1;
        player.SetActive(true);
        jumpAction.Enable();
        FergusRigidbody.gravityScale = 2f;
        FergusRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        int currentStage = PlayerPrefs.GetInt("CurrentStage", 0);
        Vector3 spawnPoint = StageManagerScript.Instance.GetStartPosition(currentStage);
        player.transform.position = spawnPoint;
    }

    private void DisableControls()
    {
        jumpAction.Disable();
        FergusRigidbody.gravityScale = 0.2f;
    }

    private void DelayedDeath()
    {
        Time.timeScale = 0;
        player.SetActive(false);
        PanelManagerScript.Instance.DeathPanel();
    }
}
