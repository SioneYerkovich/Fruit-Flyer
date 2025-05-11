using UnityEngine;

public class WatermelonValue : MonoBehaviour
{
    public GameObject sparkles;
    public Animator animator;
    public int value = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WatermelonValue watermelon = GetComponent<WatermelonValue>();
            if (watermelon != null)
            {
                ObjectiveManager.Instance.AddPoints(watermelon.value);
            }
            animator.SetBool("PopFruit", true);
            GameObject effect = Instantiate(sparkles, transform.position, Quaternion.identity);
            ParticleSystem sparklesEffect = effect.GetComponent<ParticleSystem>();
            sparklesEffect.Play();
            Destroy(effect, 0.5f);
            Destroy(gameObject, 1f);
        }
    }
}
