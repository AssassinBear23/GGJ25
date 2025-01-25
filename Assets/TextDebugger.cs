using TMPro;
using UnityEngine;

public class TextDebugger : MonoBehaviour
{
    public NodeBuoyancy nodeBuoyancy;
    public TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.text = $"BuoyancyForce: {nodeBuoyancy.BuoyancyForce.y}";
    }
}
