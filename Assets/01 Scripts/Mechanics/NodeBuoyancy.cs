using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class NodeBuoyancy : MonoBehaviour
{
    /// <summary>
    /// The buoyancy of the area the node is in.
    /// </summary>
    [field: SerializeField]
    public float AreaBuoyancy { get; private set; } = 0;
    /// <summary>
    /// The buoyancy area layer.
    /// </summary>
    [field: SerializeField]
    public LayerMask AreaLayer { get; private set; }

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Entered {other.name}'s collider trigger");
        Debug.Log((1 << other.gameObject.layer) == AreaLayer);
        if (((1 << other.gameObject.layer)) == AreaLayer)
        {
            AreaBuoyancy = other.GetComponent<LiquidBuoyancy>().Buoyancy;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Exited {other.name}'s collider trigger");
        Debug.Log((1 << other.gameObject.layer) == AreaLayer);
        if (((1 << other.gameObject.layer)) == AreaLayer)
        {
            AreaBuoyancy = 0;
        }
    }

    public Vector2 BuoyancyForce { get; private set; }
    private void FixedUpdate()
    {

        BuoyancyForce = AreaBuoyancy * Physics2D.gravity.magnitude * Vector2.up;
        rb.AddForce(BuoyancyForce);
        Debug.Log($"Current Force: {rb.totalForce}");

    }
}
