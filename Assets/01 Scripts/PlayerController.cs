using System;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5f;

    private Rigidbody2D playerBody;
    private float moveInput;

    public bool inBubbleCollumn = false;
    [SerializeField] private float CurretnBubbleSize;
    [SerializeField] private float growSpeed = 0.2f;
    [SerializeField] private float shrinkSpeed = 0.2f;
    [SerializeField] private float minSize = 0.5f;
    [SerializeField] private float maxSize = 5.0f;

    [SerializeField] private float returnBias = 0.1f;
    [SerializeField] private Vector2[] originalPoints;

    #region Visual Variables
    private const float splineOffset = 0.5f;

    [SerializeField] private SpriteShapeController spriteShape;
    [SerializeField] private Transform[] points;
    #endregion

    private void Awake() {
        LogOriginalPoints();
        UpdateVerticies();
    }

    /// <summary>
    /// Logs the original points of the player.
    /// </summary>
    private void LogOriginalPoints() {
        int childCount = transform.childCount;
        originalPoints = new Vector2[childCount];

        for (int i = 0; i < childCount; i++) {
            originalPoints[i] = transform.GetChild(i).localPosition;
        }
    }

    void Start() {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        UpdateVerticies();

        moveInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.S)) {
            foreach (Transform child in transform) {
                SpringJoint2D[] springs = child.GetComponents<SpringJoint2D>();

                foreach (SpringJoint2D spring in springs) {
                    if (CurretnBubbleSize > minSize) {
                        spring.distance -= shrinkSpeed * Time.deltaTime;
                        CurretnBubbleSize -= shrinkSpeed * Time.deltaTime;
                    }
                }
            }
        }

        if (inBubbleCollumn && Input.GetKey(KeyCode.W)) {
            foreach (Transform child in transform) {
                SpringJoint2D[] springs = child.GetComponents<SpringJoint2D>();

                foreach (SpringJoint2D spring in springs) {
                    if (CurretnBubbleSize < maxSize) {
                        spring.distance += growSpeed * Time.deltaTime;
                        CurretnBubbleSize += growSpeed * Time.deltaTime;
                    }
                }
            }
        }
    }

    void FixedUpdate() {
        playerBody.linearVelocity = new Vector2(moveInput * moveSpeed, playerBody.linearVelocity.y);

        foreach (Transform child in transform) {
            Vector2 directionToOriginal = (originalPoints[child.GetSiblingIndex()] - (Vector2)child.localPosition).normalized;
            child.localPosition = Vector2.Lerp(child.localPosition, originalPoints[child.GetSiblingIndex()], returnBias * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Updates the verticies of the sprite shape to match the points of the player.
    /// </summary>
    private void UpdateVerticies() {
        for (int i = 0; i < points.Length - 1; i++) {
            Vector2 _vertex = points[i].localPosition;
            Vector2 _towardsCenter = (Vector2.zero - _vertex).normalized;

            float _colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try {
                spriteShape.spline.SetPosition(i, _vertex - _towardsCenter * _colliderRadius);
            }
            catch {
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
