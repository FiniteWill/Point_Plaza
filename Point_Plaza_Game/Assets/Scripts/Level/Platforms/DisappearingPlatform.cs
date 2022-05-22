using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Platform object that disappears and reappears.
/// </summary>
public class DisappearingPlatform : MonoBehaviour, IPlatform
{
    [SerializeField] private bool disappearsOnContact = true;

    public bool disappearOnContact { get { return disappearOnContact; } set { disappearsOnContact = value; } }
    [SerializeField] private float timeToDisappear = 2f;
    [SerializeField] private float timeToReappear = 5f;
    [SerializeField] private GameObject platform = null;

    private Transform playerParent = null;
    private IEnumerator disappearCR;
    private Animation disappearanceAnim = null;
    private Animation reappearanceAnim = null;
    // TODO: Add transition Animation for the object when disappearing and reappearing

    private void Awake()
    {
        Assert.IsNotNull(platform, $"{name} does not have a {platform.GetType()} to make disappear.");
        Assert.IsNotNull(disappearanceAnim, $"{name} does not have a disappearance {disappearanceAnim}");
        Assert.IsNotNull(reappearanceAnim, $"{name} does not have a reappearance {reappearanceAnim}");
        disappearCR = Disappear();
        StartCoroutine(disappearCR);
;    }

    private void OnEnable()
    {
        StartCoroutine(disappearCR);
    }

    private void OnCollisionEnte2Dr(Collision2D collision)
    {
        // Child the player object to this platform on initial contact.
        if (collision.collider.GetComponent<IPlayer>() == null) { return; }
        playerParent = collision.collider.transform.parent;
        collision.collider.transform.parent = platform.transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reset the player's parent after leaving contact.
        if (collision.collider.GetComponent<IPlayer>() == null) { return; }
        if (playerParent != null) { collision.collider.transform.parent = playerParent; }
    }

    private IEnumerator Disappear()
    {
        Move();
        yield return new WaitForSeconds(timeToReappear);
        Reappear();
        StartCoroutine(disappearCR);
    }

    private void Reappear()
    {
        enabled = true;
    }

    public void Move()
    {
        enabled = false;
    }
}
