using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float length;
    private float xPosition;

    private void Start()
    {
        cam = Camera.main.gameObject;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    private void Update()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector2(xPosition + distanceToMove, transform.position.y);

        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        if (distanceMoved > xPosition + length / 2)
        {
            xPosition = xPosition + length;
        }
        else if (distanceMoved < xPosition - length / 2)
        {
            xPosition = xPosition - length;
        }
    }
}