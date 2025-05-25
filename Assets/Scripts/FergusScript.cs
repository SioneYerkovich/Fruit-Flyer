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
        jumpAction = InputSystem.actions.FindAction("Jump");
        FergusRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        startText.SetActive(!GameManagerScript.Instance.gameStarted && !GameManagerScript.Instance.characterMonologue);

        //ResetPlayer();

        if (!GameManagerScript.Instance.gameStarted && !GameManagerScript.Instance.characterMonologue)
        {
            if (jumpAction.IsPressed())
            {
                GameManagerScript.Instance.gameStarted = true;
                FergusRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        if (GameManagerScript.Instance.gameStarted)
        {
            if (jumpAction.IsPressed())
            {
                FergusRigidbody.linearVelocity = new Vector2(0, 1) * flapStrength;
                animator.SetBool("jumping", true);
            }
            else
            {
                animator.SetBool("jumping", false);
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
