using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Platform object that teleports between serialized positions.
/// </summary>
public class TeleportingPlatform : MonoBehaviour
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private Transform[] positions = null;
    [SerializeField] private float teleportDelay = 3f;
    [SerializeField] private Transform platform = null;
    
    private int curPos = 0;
    private Transform playerParent = null;

    //TODO: Add disappearance and reappearance Animations.

    private void Awake()
    {
        Assert.IsNotNull(platform, $"{name} does not have a {platform.GetType()} to teleport.");
        Assert.IsNotNull(positions, $"{name} does not have a serialized teleport position {positions.GetType()}");
        StartCoroutine(Disappear());
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
    /// Coroutine that initiates a teleport, waits for a delay, and then calls itself again. 
    /// It will stop if the object is disabled and will begin again on enable.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Disappear()
    {
        Move();
        yield return new WaitForSeconds(teleportDelay);
        StartCoroutine(Disappear());
    }

    public void Move()
    {
        platform.position = positions[curPos].position;
        curPos++;
        if (curPos > positions.Length - 1) { curPos = 0; }

    }
}
