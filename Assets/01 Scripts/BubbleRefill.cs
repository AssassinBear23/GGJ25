using UnityEngine;
using UnityEngine.Events;

public class UnityEventTrigger : MonoBehaviour {
    GameManager gameManager;

    private void OnEnable() {
        gameManager = GameManager.Instance;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            gameManager.SetInBubbleCollumn(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            gameManager.SetInBubbleCollumn(false);
        }
    }
}
