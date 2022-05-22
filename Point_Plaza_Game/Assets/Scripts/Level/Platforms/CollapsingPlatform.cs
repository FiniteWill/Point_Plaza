using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Platform that collapses or disappears on contact. 
/// </summary>
public class CollapsingPlatform : MonoBehaviour
{
    /// <summary>
    /// The list of tags for entities that can collapse the platform.
    /// </summary>
    [SerializeField] private string[] collapsedByEntities;
    /// <summary>
    /// How fast the platform falls when collapsing.
    /// </summary>
    [SerializeField] private float speedOfFall = 0.1f;
    /// <summary>
    /// How long it takes for a collapse to fully finish. The platform is disabled until it reappears.
    /// </summary>
    [SerializeField] private float timeToCollapse = 0.1f;
    /// <summary>
    /// How long it takes for the platform to start collapsing.
    /// </summary>
    [SerializeField] private float timeBeforeCollapse = 2f;
    /// <summary>
    /// How long it takes for the platform to reappear to its starting position after a collapse has finished. Set to 0 if it does not reappear.
    /// </summary>
    [SerializeField] private float timeToReappear = 5f;
    [SerializeField] private GameObject platform = null;
    [SerializeField] private Animation disappearanceAnim = null;
    [SerializeField] private Animation reappearanceAnim = null;
    
    private Vector3 originalPosition = Vector3.zero;
    private IEnumerator CR_Collapse;
    private bool collapseIsRunning = false;
    private bool reappearIsRunning = false;
    private Transform playerParent = null;

    private void Awake()
    {
        Assert.IsNotNull(platform, $"{name} does not have a {platform.GetType()} to make collapse.");
        Assert.IsNotNull(disappearanceAnim, $"{name} does not have a disappearance {disappearanceAnim}");
        Assert.IsNotNull(reappearanceAnim, $"{name} does not have a reappearance {reappearanceAnim}");
        originalPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Set the player's transform to be a child of the platform, playe the disappearance Animation, and handle disappearance.
        if (collision.collider.GetComponent<IPlayer>() == null) { return; }
        playerParent = collision.collider.transform.parent;
        collision.collider.transform.parent = platform.transform;

        disappearanceAnim.Play();
        if (!collapseIsRunning) { StartCoroutine(CR_Collapse); }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reset the player's parent after leaving contact.
        if(collision.collider.GetComponent<IPlayer>() == null) { return; }
        if (playerParent != null) { collision.collider.transform.parent = playerParent; }
    }


    private IEnumerator Collapse()
    {
        // Check that the platform is not currently reappearing before initiating collapse.
        if (!reappearIsRunning)
        {
            collapseIsRunning = true;
            // Wait for a bit before initiating the collapse.
            if (timeBeforeCollapse > 0)
            {
                yield return new WaitForSeconds(timeBeforeCollapse);
            }
            // Initiate the positional change in the platform after collapse.
            Fall();
            collapseIsRunning = false;
        }
    }

    private void Reappear()
    {
        // Wait for the time to reappear, re-enable the object, play the reappearance Animation, and snap back to the original position.
        reappearIsRunning = true;
        while(timeToReappear > 0)
        {
            timeToReappear -= Time.deltaTime;
        }
        enabled = true;
        transform.position = originalPosition;
        reappearIsRunning = false;
    }

    public void Fall()
    {
        // Wait for the duration of a collapse, decrease the y position of the platform to make it fall,
        // play the disappearance Animation, and then start reappearing the platform.
        float tempTimer = timeToCollapse;
        while(tempTimer > 0)
        {
            tempTimer -= Time.deltaTime;
            transform.position -= new Vector3(0, speedOfFall, 0);
        }
        disappearanceAnim.Play();
        enabled = false;
        Reappear();
    }
}
