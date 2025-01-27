using System;
using UnityEngine;

/// <summary>
/// This class handles the behavior of following the mouse position or the player's position
/// based on the focus state of the application.
/// </summary>
public class FollowMousePosition : MonoBehaviour
{
    /// <summary>
    /// The player in the scene
    /// </summary>
    [SerializeField] private Transform player;
    /// <summary>
    /// If the game is focused, the camera will follow the mouse position.
    /// </summary>
    private bool gameFocused;


    private void Update()
    {
        if (gameFocused)
        {
            FollowMouse();
        }
        else
        {
            FollowPlayer();
        }
    }

    /// <summary>
    /// Called when the application gains or loses focus.
    /// </summary>
    /// <param name="focus">True if the application is focused, false otherwise.</param>
    private void OnApplicationFocus(bool focus)
    {
        gameFocused = focus;
    }

    /// <summary>
    /// Follows the player's position.
    /// </summary>
    private void FollowPlayer()
    {
        transform.position = player.position;
    }

    /// <summary>
    /// Follows the mouse position.
    /// </summary>
    private void FollowMouse()
    {
        Vector2 position = GetMousePosition();
        transform.position = position;
    }

    /// <summary>
    /// Gets the current mouse position in world coordinates.
    /// </summary>
    /// <returns>The mouse position in world coordinates.</returns>
    private Vector2 GetMousePosition()
    {
        Vector2 mouseViewportPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mouseViewportPosition = new Vector2(Mathf.Clamp01(mouseViewportPosition.x), Mathf.Clamp01(mouseViewportPosition.y));
        return Camera.main.ViewportToWorldPoint(mouseViewportPosition);
    }
}
