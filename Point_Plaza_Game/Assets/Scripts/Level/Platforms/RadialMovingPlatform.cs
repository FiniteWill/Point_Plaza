using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Platform that turns a platform. Can be used on a parent object 
/// to rotate a child object around a point.
/// </summary>
public class RadialMovingPlatform : MonoBehaviour
{
    private static WaitForEndOfFrame s_waitFrame = new WaitForEndOfFrame();

    [SerializeField] private bool isDebugging = false;
    [Tooltip("Whether the platform should turn along with its parent or keep the same rotation.")]
    [SerializeField] private bool rotatesWithTurn = true;
    [Tooltip("How long in seconds the platform will wait before moving again.")]
    [SerializeField] private float movementDelay = 3f;
    [SerializeField] private Transform platform = null;
    [Tooltip("How many degrees the platform moves during each turn.")]
    [SerializeField] private Vector3 rotation = new Vector3();
    [Tooltip("How many steps each turn is performed in.")]
    [SerializeField][Min(1)] private int stepsPerTurn = 3;

    private int degreesTurned = 0;
    private Transform playerParent = null;
    private bool isMoving = true;
    public bool Moving { get { return isMoving; } set { isMoving = value; } }

    private void Awake()
    {
        Assert.IsNotNull(platform, $"{name} does not have a {platform.GetType()} to move.");
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
    /// Turns the platform by rotating the number of degrees per turn / number of steps each frame.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MovePlatform()
    {
        for(int i=0; i< stepsPerTurn; ++i)
        {
            platform.Rotate(rotation / stepsPerTurn);
            if (isDebugging) { Debug.Log($"{name} is at {platform.position} and it has rotated {degreesTurned}"); }
            yield return s_waitFrame;
        }
        yield return new WaitForSeconds(movementDelay);
        StartCoroutine(MovePlatform());
    }

    public void Move()
    {

    }
}
