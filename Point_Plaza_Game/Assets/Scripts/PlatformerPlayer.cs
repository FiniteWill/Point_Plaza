using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Script that handles Player controls for a character in one of the platformer games.
/// </summary>
public class PlatformerPlayer : MonoBehaviour
{
    [SerializeField] private bool isDebugging = true;

    // Movement Constants
    private const float HORIZONTAL_SPEED = 5.0f;
    private const float JUMP_SPEED = 200.0f;

    // Movement Bindings
    private KeyCode moveLeftControl = KeyCode.A;
    private KeyCode moveRightControl = KeyCode.D;
    private KeyCode jumpControl = KeyCode.Space;

    // Player components
    [SerializeField] private Rigidbody2D rgbd2D = null;
    [SerializeField] private BoxCollider2D playerCollider = null;



    private void Awake()
    {
        Assert.IsNotNull(playerCollider, $"{this.name} does not have a serialized {nameof(playerCollider)} but requires one.");
        Assert.IsNotNull(rgbd2D, $"{this.name} does not have a serialized {nameof(rgbd2D)} but requires one.");
        if (rgbd2D != null)
        {
            rgbd2D.freezeRotation = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Movement(); GroundCheck();
    }

    private void Movement()
    {
        // Horizontal Movement
        if (Input.GetKey(moveLeftControl))
        { rgbd2D.AddForce(new Vector2(-HORIZONTAL_SPEED, 0.0f)); }
        else if (Input.GetKey(moveRightControl))
        { rgbd2D.AddForce(new Vector2(HORIZONTAL_SPEED, 0.0f)); }

        // Jumping
        if (Input.GetKeyDown(jumpControl))
        {
            if (isGrounded) { rgbd2D.AddForce(new Vector2(0.0f, JUMP_SPEED)); }
        }
    }

    private const float GROUND_CHECK_DIST = 0.1f;
    private const float GROUND_CHECK_OFFSET = 0.05f;
    private const string GROUND_TAG = "Ground";
    private bool isGrounded = false;
    private void GroundCheck()
    {
        isGrounded = false;

        // Do a raycast to check if the player's left side is on the ground
        RaycastHit2D leftGroundCheck =
        Physics2D.Raycast(new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET), Vector2.down, GROUND_CHECK_DIST);
        if(leftGroundCheck.collider != null)
        {
            if (leftGroundCheck.collider.CompareTag(GROUND_TAG)) { isGrounded = true; }
        }
        if(isDebugging)
        {
            if (leftGroundCheck.collider == null) { Debug.Log("$Left ground check is null"); }
            else { Debug.Log($"Left ground check {leftGroundCheck.collider.tag}"); }
            Debug.DrawLine(new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET), 
                new Vector3(playerCollider.bounds.min.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET - GROUND_CHECK_DIST),Color.red);
        }

        // Do a raycast to check if the player's right side is on the ground
        RaycastHit2D rightGroundCheck =
        Physics2D.Raycast(new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET), Vector2.down, GROUND_CHECK_DIST);
        if(rightGroundCheck.collider != null)
        { 
            if (rightGroundCheck.collider.CompareTag(GROUND_TAG)){ isGrounded = true; } 
        }
        if(isDebugging)
        {
            if (rightGroundCheck.collider == null) { Debug.Log($"Right ground check is null"); }
            else { Debug.Log($"Right ground check {rightGroundCheck.collider.tag}"); }
            Debug.DrawLine(new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET),
                new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET -GROUND_CHECK_DIST), Color.red);
        
        }
    }
}
