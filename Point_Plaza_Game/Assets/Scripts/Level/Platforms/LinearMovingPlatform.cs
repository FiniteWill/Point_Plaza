using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Platform that moves between points in linear motions.
/// </summary>
public class LinearMovingPlatform : MonoBehaviour
{
    private static readonly WaitForEndOfFrame s_waitFrame = new WaitForEndOfFrame();

    [SerializeField] private bool isDebugging = false;
    [SerializeField] private Transform[] positions = null;
    [SerializeField] private WaitForSeconds movementDelay = new WaitForSeconds(3f);
    [SerializeField] private Transform platform = null;
    [SerializeField] [Min(0f)] float speed = 1f;

    private int curPos;
    private Transform playerParent = null;
    private bool isMoving = true;
    public bool Moving { get { return isMoving; } set { isMoving = value; } }

    //TODO: Add disappearance and reappearance Animations.

    private void Awake()
    {
        Assert.IsNotNull(platform, $"{name} does not have a {platform.GetType()} to move.");
        Assert.IsNotNull(positions, $"{name} does not have a serialized teleport position {positions.GetType()}");
        StartCoroutine(MovePlatform());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Child the player object to this platform on initial contact.
        if (other.GetComponent<IPlayer>() == null) { return; }
        if (isDebugging) { Debug.Log($"{name} collided with {other.name} which has an attached {nameof(IPlayer)}"); }
        other.transform.parent = platform.transform;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Reset the player object's parent after leaving contact.
        if (other.GetComponent<IPlayer>() == null) { return; }
        if (isDebugging) { Debug.Log($"{name} has stopped colliding with {other.name} which has an attached {nameof(IPlayer)}"); }
        other.transform.parent = null;
    }

    /// <summary>
    /// Sets isMoving to false, iterates the list of target positions, and waits before allowing the platform to move again.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MovePlatform()
    {
        if (positions.Length >= 2)
        {
            while (transform.position != positions[curPos].position)
            {
                Move();
                yield return s_waitFrame;
            }
            yield return movementDelay;
            curPos++;
            if (curPos > positions.Length - 1) { curPos = 0; }
            StartCoroutine(MovePlatform());
        }
    }

    public void Move()
    {
        float step = speed * Time.deltaTime;
        platform.position = Vector3.MoveTowards(platform.position, positions[curPos].position, step);
        if (isDebugging) { Debug.Log($"{name} is at {platform.position} and its target is {positions[curPos].position}"); }
    }
}
