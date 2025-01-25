using System;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private float moveInput;

    private const float splineOffset = 0.5f;

    [SerializeField]
    private SpriteShapeController spriteShape;
    [SerializeField]
    private Transform[] points;


    private void Awake() {
        UpdateVerticies();
    }


    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        UpdateVerticies();

        moveInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.S)) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -moveSpeed);
        }
    }

    void FixedUpdate() {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
    private void UpdateVerticies() {
        for (int i = 0; i < points.Length - 1; i++) {
            Vector2 _vertex = points[i].localPosition;
            Vector2 _towardsCenter = (Vector2.zero - _vertex).normalized;

            float _colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try {
                spriteShape.spline.SetPosition(i, _vertex - _towardsCenter * _colliderRadius);
            } catch {
                Debug.Log("Error: " + i);
                spriteShape.spline.SetPosition(i, _vertex - _towardsCenter * (_colliderRadius + splineOffset));
            }

            Vector2 _lt = spriteShape.spline.GetLeftTangent(i);
            Vector2 _newRt = Vector2.Perpendicular(_towardsCenter) * _lt.magnitude;
            Vector2 _newLt = Vector2.zero - _newRt;

            spriteShape.spline.SetRightTangent(i, _newRt);
            spriteShape.spline.SetLeftTangent(i, _newLt);
        }
    }
}
