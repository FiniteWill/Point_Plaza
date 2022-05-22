using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Platform object that teleports between serialized positions.
/// </summary>
public class TeleportingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] positions = null;
    [SerializeField] private float teleportDelay = 3f;
    [SerializeField] private Transform platform = null;
    
    private IEnumerator CR_Disappear;
    private int curPos;
    private Transform playerParent = null;

    //TODO: Add disappearance and reappearance Animations.

    private void Awake()
    {
        Assert.IsNotNull(platform, $"{name} does not have a {platform.GetType()} to teleport.");
        Assert.IsNotNull(positions, $"{name} does not have a serialized teleport position {positions.GetType()}");
        CR_Disappear = Disappear();
        StartCoroutine(CR_Disappear);
    }

    private void OnEnable()
    {
        StartCoroutine(CR_Disappear);
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

    /// <summary>
    /// Coroutine that initiates a teleport, waits for a delay, and then calls itself again. 
    /// It will stop if the object is disabled and will begin again on enable.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Disappear()
    {
        Move();
        yield return new WaitForSeconds(teleportDelay);
        StartCoroutine(CR_Disappear);
    }

    public void Move()
    {
        curPos += 1 % positions.Length;
        platform.position = positions[curPos].position;
    }
}
