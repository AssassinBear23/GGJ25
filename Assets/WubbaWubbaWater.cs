using UnityEngine;

public class WubbaWubbaWater : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    private Vector4 wubbaWubbaPoints;
    private Vector2 wubbaWubbaTimes;

    bool one = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }
        if (one)
        {
            var point = boxCollider2D.ClosestPoint(collision.transform.position);
            wubbaWubbaPoints.x = point.x;
            wubbaWubbaPoints.y = point.y;
            spriteRenderer.material.SetVector("_WubbaWubbaPoints", wubbaWubbaPoints);
            wubbaWubbaTimes.x = Time.time;
            spriteRenderer.material.SetVector("_WubbaWubbaTimes", wubbaWubbaTimes);
        }
        else
        {
            var point = boxCollider2D.ClosestPoint(collision.transform.position);
            wubbaWubbaPoints.z = point.x;
            wubbaWubbaPoints.w = point.y;
            spriteRenderer.material.SetVector("_WubbaWubbaPoints", wubbaWubbaPoints);
            wubbaWubbaTimes.y = Time.time;
            spriteRenderer.material.SetVector("_WubbaWubbaTimes", wubbaWubbaTimes);
        }

        one = !one;
    }
}
