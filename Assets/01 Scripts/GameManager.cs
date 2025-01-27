using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PlayerController player;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void SetInBubbleCollumn(bool value) {
        player.inBubbleCollumn = value;
    }

    public void StartGame() {
        Debug.Log("Game Started");
    }

    public void GameOver() {
        Debug.Log("Game Over");
    }

    public void RestartGame() {
        Debug.Log("Game Restarted");
    }

}
