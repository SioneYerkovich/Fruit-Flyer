using UnityEngine;

public class WallScript : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5;
    public float deadZone = -15;
    public AudioSource wallSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wallSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SpeedBoostManagerScript.Instance.isBoosted)
        {
            transform.position = transform.position + (Vector3.left * SpeedBoostManagerScript.Instance.normalSpeed) * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position + (Vector3.left * SpeedBoostManagerScript.Instance.boostedSpeed) * Time.deltaTime;
        }

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wallSound.Play();
            animator.SetBool("Death", true);
        }
    }
}