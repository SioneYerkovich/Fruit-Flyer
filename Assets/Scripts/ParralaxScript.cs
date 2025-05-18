using UnityEngine;

public class ParralaxScript : MonoBehaviour
{
    
    public float scrollSpeed = 0.1f;
    private float width;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundMovement();
    }

    public void BackgroundMovement()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x < -width)
        {
            transform.position += new Vector3(width * 2f, 0, 0);
        }
    }
}
