using UnityEngine;

public class CoconutValue : MonoBehaviour
{
    public GameObject sparkles;
    public Animator animator;
    public int value = 3;
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
            CoconutValue coconut = GetComponent<CoconutValue>();
            if (coconut != null)
            {
                ObjectiveManager.Instance.AddPoints(coconut.value);
            }

            if (this.gameObject.CompareTag("Rotten"))
            {
                animator.SetBool("Rotten", true);
            }
            else
            {
                animator.SetBool("PopFruit", true);
            }
            GameObject effect = Instantiate(sparkles, transform.position, Quaternion.identity);
            ParticleSystem sparklesEffect = effect.GetComponent<ParticleSystem>();
            sparklesEffect.Play();
            Destroy(effect, 0.5f);
            Destroy(gameObject, 1f);
        }
    }
}
