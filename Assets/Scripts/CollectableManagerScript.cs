using System.Collections;
using UnityEngine;

public class CollectableManagerScript : MonoBehaviour
{
    public AudioSource soundEffect;
    public GameObject powerupSparkles;
    public Animator animator;
    public float deadZone = -15;
    public float powerupDuration = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SetObjectSpeed();
        DestroyObject();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.CompareTag("Fruit"))
        {
            TriggerSound();
        }
        else if (this.gameObject.CompareTag("SpeedBoost"))
        {
            SpeedBoostManagerScript.Instance.ActivateBoost(powerupDuration);
            animator.SetBool("grabbed", true);
            TriggerSound();
            GameObject effect = Instantiate(powerupSparkles, transform.position, Quaternion.identity);
            ParticleSystem sparklesEffect = effect.GetComponent<ParticleSystem>();
            sparklesEffect.Play();
            Destroy(effect, 0.5f);
            Destroy(gameObject, 1f);
        }
        else if (this.gameObject.CompareTag("DoublePoints"))
        {
            DoublePointManagerScript.Instance.ActivateBonus(powerupDuration);
            this.gameObject.GetComponent<ParticleSystem>().Stop();
            animator.SetBool("grabbed", true);
            TriggerSound();
            GameObject effect = Instantiate(powerupSparkles, transform.position, Quaternion.identity);
            ParticleSystem sparklesEffect = effect.GetComponent<ParticleSystem>();
            sparklesEffect.Play();
            Destroy(effect, 0.5f);
            Destroy(gameObject, 1f);
        }
        else if (this.gameObject.CompareTag("Rotten"))
        {
            TriggerSound();
        }
    }

    private void TriggerSound()
    {
        soundEffect.Play();
    }

    public void SetObjectSpeed()
    {
        if (!SpeedBoostManagerScript.Instance.isBoosted)
        {
            transform.position = transform.position + (Vector3.left * SpeedBoostManagerScript.Instance.normalSpeed) * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position + (Vector3.left * SpeedBoostManagerScript.Instance.boostedSpeed) * Time.deltaTime;
        }
    }

    public void DestroyObject()
    {
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
