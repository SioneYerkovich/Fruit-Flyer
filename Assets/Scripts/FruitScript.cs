using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public AudioSource fruitSound;
    public float moveSpeed = 5;
    public float deadZone = -9;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fruitSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
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
