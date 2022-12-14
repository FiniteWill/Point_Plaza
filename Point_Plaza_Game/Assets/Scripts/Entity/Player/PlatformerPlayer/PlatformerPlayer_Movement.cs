using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Enums;

public class PlatformerPlayer_Movement : MonoBehaviour, PointPlaza_Input.IPlayerActions
{
    // Movement Constants
    private const float MAX_HORIZ_SPEED = 25f;
    private const float HORIZONTAL_SPEED = 5f;
    private const float JUMP_SPEED = 300.0f;
    private static Vector2 s_leftMovementForce = new Vector2(-HORIZONTAL_SPEED, 0f);
    private static Vector2 s_rightMovementForce = new Vector2(HORIZONTAL_SPEED, 0f);
    private static PlayerInput s_playerInput;
    private static InputAction s_movement;
    // GroundCheck() constants
    private const float GROUND_CHECK_DIST = 0.1f;
    private const float GROUND_CHECK_OFFSET = 0.05f;
    private const string GROUND_TAG = "Ground";
    private bool canDoubleJump = false;
    // Charge Attack Constants
    private const int CHARGE_ATTACK_DAMAGE = 2;
    private static Vector2 s_chargeAttackForce = new Vector2(4f, 0f);
    [SerializeField] private Collider2D chargeAttackCollider = null;
    private const float CHARGE_COOLDOWN = 2f;
    private bool chargeCoolingDown = false;

    [SerializeField] private bool isDebugging = true;

    // Animation state
    private PlatformerAnimationState animationState = PlatformerAnimationState.Idle;
    public PlatformerAnimationState GetAnimationState() { return animationState; }

    // Player components
    [SerializeField] private Rigidbody2D rgbd2D = null;
    [SerializeField] private BoxCollider2D playerCollider = null;



    private bool isGrounded = false;

    private void Awake()
    {
        Assert.IsNotNull(playerCollider, $"{name} does not have a serialized {nameof(playerCollider)} but requires one.");
        Assert.IsNotNull(rgbd2D, $"{name} does not have a serialized {nameof(rgbd2D)} but requires one.");
        if (rgbd2D != null)
        {
            rgbd2D.freezeRotation = true;
        }
        s_playerInput = transform.root.GetComponentInChildren<PlayerInput>();
        s_movement = s_playerInput.currentActionMap.FindAction("Move", true);
    }

    // Update is called once per frame
    private void Update()
    {
        NewMove(s_movement.ReadValue<Vector2>().x);
        GroundCheck();
    }

    public void Jump()
    {
        if (isGrounded || canDoubleJump)
        {
            canDoubleJump = false;
            // Apply jump force and then change the animation state based on which way the player was moving
            if (rgbd2D.velocity.x > 5f || rgbd2D.velocity.x < -5f)
            {
                rgbd2D.AddForce(new Vector2(0.0f, JUMP_SPEED + Mathf.Abs(rgbd2D.velocity.magnitude) * 0.25f));
            }
            else { rgbd2D.AddForce(new Vector2(0, JUMP_SPEED)); }
            if (animationState == PlatformerAnimationState.RunLeft)
            {
                animationState = PlatformerAnimationState.JumpLeft;
            }
            else if (animationState == PlatformerAnimationState.RunRight)
            {
                animationState = PlatformerAnimationState.JumpRight;
            }
            else
            {
                animationState = PlatformerAnimationState.JumpUp;
            }
        }
    }

    // TODO: Create toggleable and/or powerup for double jumping that uses this. (Post-SGX)
    private void ResetDoubleJump(float seconds)
    {
        while (seconds >= 0)
        {
            seconds -= Time.deltaTime;
        }
        canDoubleJump = true;
    }

    private void NewMove(float horizontal)
    {
        if (horizontal > 0)
        {
            if (rgbd2D.velocity.x < MAX_HORIZ_SPEED)
            {
                rgbd2D.AddForce(s_rightMovementForce);
            }
            animationState = PlatformerAnimationState.RunRight;
        }
        else if (horizontal < 0)
        {
            if (rgbd2D.velocity.x > -MAX_HORIZ_SPEED)
            {
                rgbd2D.AddForce(s_leftMovementForce);
            }
            animationState = PlatformerAnimationState.RunLeft;
        }
    }

    private void ChargeAttack()
    {
        if (isGrounded)
        {
            if (!chargeCoolingDown)
            {
                if (rgbd2D.velocity.x < -(MAX_HORIZ_SPEED / 4))
                {
                    rgbd2D.AddForce(-s_chargeAttackForce);
                }
                else if (rgbd2D.velocity.x > (MAX_HORIZ_SPEED / 4))
                {
                    rgbd2D.AddForce(s_chargeAttackForce);
                }
                ChargeAttackCooldown(CHARGE_COOLDOWN);
            }
        }
    }

    private IEnumerator EnableChargeAttackCollider(float seconds)
    {
        chargeAttackCollider.enabled = true;
        yield return new WaitForSeconds(seconds);
        StopCoroutine(EnableChargeAttackCollider(seconds));
    }

    private IEnumerator ChargeAttackCooldown(float seconds)
    {
        chargeCoolingDown = true;
        yield return new WaitForSeconds(seconds);
        chargeCoolingDown = false;
    }

    /// <summary>
    /// Checks if the player is touching the ground.
    /// </summary>
    private void GroundCheck()
    {
        isGrounded = false;

        // Do a raycast to check if the player's left side is on the ground
        RaycastHit2D leftGroundCheck =
        Physics2D.Raycast(new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET), Vector2.down, GROUND_CHECK_DIST);
        if (leftGroundCheck.collider != null)
        {
            if (leftGroundCheck.collider.CompareTag(GROUND_TAG)) { isGrounded = true; }
        }
        if (isDebugging)
        {
            if (leftGroundCheck.collider == null) { Debug.Log("$Left ground check is null"); }
            else { Debug.Log($"Left ground check {leftGroundCheck.collider.tag}"); }
            Debug.DrawLine(new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET),
                new Vector3(playerCollider.bounds.min.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET - GROUND_CHECK_DIST), Color.red);
        }

        // Do a raycast to check if the player's right side is on the ground
        RaycastHit2D rightGroundCheck =
        Physics2D.Raycast(new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET), Vector2.down, GROUND_CHECK_DIST);
        if (rightGroundCheck.collider != null)
        {
            if (rightGroundCheck.collider.CompareTag(GROUND_TAG)) { isGrounded = true; }
        }
        if (isDebugging)
        {
            if (rightGroundCheck.collider == null) { Debug.Log($"Right ground check is null"); }
            else { Debug.Log($"Right ground check {rightGroundCheck.collider.tag}"); }
            Debug.DrawLine(new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET),
                new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y - GROUND_CHECK_OFFSET - GROUND_CHECK_DIST), Color.red);

        }
    }

    /// <summary>
    /// Handles movement caused by taking damage;
    /// </summary>
    private void DamagedMovement(float power, Vector2 direction)
    {
        rgbd2D.AddForce(power * -direction);
        var tempSR = GetComponentInChildren<SpriteRenderer>();
        var tempColor = tempSR.color;
        DamageFlash(2f, tempSR, tempColor);
        animationState = PlatformerAnimationState.TakeDamage;
    }

    // How much the color of a sprite changes towards and from red each frame when damaged
    private int DAMAGE_FLASH = 1;
    private void DamageFlash(float secondsToFlash, SpriteRenderer spriteRenderer, Color startColor)
    {
        if (secondsToFlash <= 0) { return; }
        else
        {
            while (secondsToFlash >= secondsToFlash / 2)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r + DAMAGE_FLASH, spriteRenderer.color.g, spriteRenderer.color.b);
                secondsToFlash -= Time.deltaTime;
            }

            while (secondsToFlash >= 0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r - DAMAGE_FLASH, spriteRenderer.color.g, spriteRenderer.color.b);
                secondsToFlash -= Time.deltaTime;
            }

            return;
        }
    }

    // Implemented functions of IPlayerActions

    public void OnMove(InputAction.CallbackContext context)
    {

    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        ChargeAttack();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
    }

    public void OnStart(InputAction.CallbackContext context)
    {
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
    }
}
