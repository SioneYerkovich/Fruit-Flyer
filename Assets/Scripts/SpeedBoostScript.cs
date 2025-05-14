using UnityEngine;

public class SpeedBoostScript : MonoBehaviour
{
    public Animator animator;
    public BombScript BombScript;
    public AudioSource PowerupSound;
    public GameObject sparkles;
    public float duration = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PowerupSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FruitManagerScript.Instance.isBoosted)
        {
            transform.position = transform.position + (Vector3.left * FruitManagerScript.Instance.normalSpeed) * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position + (Vector3.left * FruitManagerScript.Instance.boostedSpeed) * Time.deltaTime;
        }

        if (transform.position.x < FruitManagerScript.Instance.deadZone)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FruitManagerScript.Instance.ActivateBoost(duration);
            animator.SetBool("grabbed", true);
            TriggerPowerupSound();
            GameObject effect = Instantiate(sparkles, transform.position, Quaternion.identity);
            ParticleSystem sparklesEffect = effect.GetComponent<ParticleSystem>();
            sparklesEffect.Play();
            Destroy(effect, 0.5f);
            Destroy(gameObject, 1f);
        }
    }

    private void TriggerPowerupSound()
    {
        PowerupSound.Play();
    }

}
