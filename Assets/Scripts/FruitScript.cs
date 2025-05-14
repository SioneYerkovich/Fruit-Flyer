using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public AudioSource fruitSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fruitSound = GetComponent<AudioSource>();
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerFruitSound();
        }
    }

    private void TriggerFruitSound()
    {
        fruitSound.Play();
    }
}
