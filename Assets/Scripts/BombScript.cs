using UnityEngine;

public class BombScript : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5;
    public float deadZone = -9;
    public AudioSource explosionSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        explosionSound = GetComponent<AudioSource>();
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

        if (transform.position.x < CollectableManagerScript.Instance.deadZone)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerExplosionSound();
            animator.SetBool("Death", true);
            Destroy(gameObject, 1f);
        }
    }

    private void TriggerExplosionSound()
    {
        explosionSound.Play();
    }
}
