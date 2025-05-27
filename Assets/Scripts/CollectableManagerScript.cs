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

    //Collision event handler for collectable items
    public void OnTriggerEnter2D(Collider2D collision) //all colliders are assigned "is trigger" to comply
    {
        if (this.gameObject.CompareTag("Fruit")) //If the tag is fruit (tags assigned to prefabs in inspector)
        {
            TriggerSound(); //play the sound
        }
        else if (this.gameObject.CompareTag("SpeedBoost")) //if the tag is speedboost
        {
            GetComponent<Collider2D>().enabled = false; //disable the collider to prevent double triggers
            SpeedBoostManagerScript.Instance.ActivateBoost(powerupDuration); //Activate the timer
            animator.SetBool("grabbed", true); //commence animation
            TriggerSound(); //play the sound
            GameObject effect = Instantiate(powerupSparkles, transform.position, Quaternion.identity);
            ParticleSystem sparklesEffect = effect.GetComponent<ParticleSystem>();
            sparklesEffect.Play(); //play the assigned and instanced sparkle effect
            Destroy(effect, 0.5f); //Destroy sparkles after a delay
            Destroy(gameObject, 1f); //Destroy powerup after delay
        }
        else if (this.gameObject.CompareTag("DoublePoints"))
        {
            GetComponent<Collider2D>().enabled = false; //disable the collider to prevent double triggers
            DoublePointManagerScript.Instance.ActivateBonus(powerupDuration); //Activate the timer
            this.gameObject.GetComponent<ParticleSystem>().Stop();
            animator.SetBool("grabbed", true); //commence animation
            TriggerSound(); //play the sound
            GameObject effect = Instantiate(powerupSparkles, transform.position, Quaternion.identity);
            ParticleSystem sparklesEffect = effect.GetComponent<ParticleSystem>();
            sparklesEffect.Play(); //play the assigned and instanced sparkle effect
            Destroy(effect, 0.5f); //Destroy sparkles after a delay
            Destroy(gameObject, 1f); //Destroy powerup after delay
        }
        else if (this.gameObject.CompareTag("Rotten")) //If the tag is rotten
        {
            TriggerSound(); //play the assigned sound
        }
    }

    //Method to call when sounds need to be triggered
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
