using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float scrollSpeed = 2f;
    private float width;

    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x < -width)
        {
            transform.position += new Vector3(width * 2f, 0, 0);
        }
    }
}