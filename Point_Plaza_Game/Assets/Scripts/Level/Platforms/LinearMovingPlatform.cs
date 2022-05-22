using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Platform that moves between points in linear motions.
/// </summary>
public class LinearMovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] positions = null;
    [SerializeField] private float movementDelay = 3f;
    [SerializeField] private Transform platform = null;
    [SerializeField] [Min(0f)] float speed = 1f;

    private IEnumerator CR_MoveDelay;
    private int curPos;
    private Transform playerParent = null;
    private bool isMoving = true;
    public bool Moving { get { return isMoving; } set { isMoving = value; } }

    //TODO: Add disappearance and reappearance Animations.

    private void Awake()
    {
        Assert.IsNotNull(platform, $"{name} does not have a {platform.GetType()} to move.");
        Assert.IsNotNull(positions, $"{name} does not have a serialized teleport position {positions.GetType()}");
        CR_MoveDelay = MoveDelayCR();
        StartCoroutine(CR_MoveDelay);
    }

    private void OnEnable()
    {
        StartCoroutine(CR_MoveDelay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Child the player object to this platform on initial contact.
        if (collision.collider.GetComponent<IPlayer>() == null) { return; }
        playerParent = collision.collider.transform.parent;
        collision.collider.transform.parent = platform.transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reset the player object's parent after leaving contact.
        if (collision.collider.GetComponent<IPlayer>() == null) { return; }
        if (playerParent != null) { collision.collider.transform.parent = playerParent; }
    }

    private void Update()
    {
        if (isMoving && platform.position != positions[curPos].position)
        {
            float step = speed * Time.deltaTime;
            Vector3.MoveTowards(platform.position, positions[curPos].position, step);
        }
        else
        {
            StartCoroutine(CR_MoveDelay);
        }
    }

    /// <summary>
    /// Sets isMoving to false, iterates the list of target positions, and waits before allowing the platform to move again.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveDelayCR()
    {
        isMoving = false;
        curPos += 1 % positions.Length;
        yield return new WaitForSeconds(movementDelay);
        isMoving = true;
    }

    public void Move()
    {
    }
}
