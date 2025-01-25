using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class LiquidBuoyancy : MonoBehaviour
{
    [field: SerializeField]
    public float Buoyancy { get; private set; }
}
