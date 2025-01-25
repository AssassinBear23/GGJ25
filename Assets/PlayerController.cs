using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private float moveInput;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        moveInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.S)) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -moveSpeed);
        }
    }

    void FixedUpdate() {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}
