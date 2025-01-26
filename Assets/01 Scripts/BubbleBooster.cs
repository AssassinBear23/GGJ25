using UnityEngine;

public class BubbleBooster : MonoBehaviour {
    private GameManager gameManager;
    public float boostForce = 20;

    private void OnEnable() {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Rigidbody2D>(out var rb)) {
            rb.AddForce(transform.up * boostForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.TryGetComponent<Rigidbody2D>(out var rb)) {
            rb.AddForce(transform.up * boostForce, ForceMode2D.Impulse);
        }
    }
}
