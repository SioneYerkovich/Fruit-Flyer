using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class FergusScript : MonoBehaviour
{
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            Die();
            animator.SetBool("Death", true);
            Destroy(gameObject, 2.5f);
        }
    }

    private void Die()
    {
        jumpAction.Disable();
        FergusRigidbody.gravityScale = 0.4f;
        GameManagerScript.Instance.LoadCheckpoint(player);
    }
}
