using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class ChargingEnemy : MonoBehaviour
{
    private static WaitForSeconds s_checkDelay;

    [SerializeField] private bool isDebugging = false;
    [SerializeField] private Vector2 directionToCharge = Vector2.zero;
    [SerializeField] private Vector2 raycastOffset = Vector2.zero;
    [SerializeField] private float chargeDistance = 5f;
    [SerializeField] private Rigidbody2D rgbd2D = null;
    [SerializeField] private float chargeSpeed = 20.0f;
    // The delay between checks that the player is in the Charger's line of sight.
    [SerializeField] [Min(1)] private float hesitation = 1f;

    private void Awake()
    {
        Assert.IsNotNull($"{this.name} does not have a {nameof(rgbd2D)} but is required.");
        StartCoroutine(CheckForPlayer());
        s_checkDelay = new WaitForSeconds(hesitation);
    }


    private IEnumerator CheckForPlayer()
    {
        if(isDebugging)
        {
            Debug.DrawLine(new Vector2(transform.position.x, transform.position.y), 
                new Vector2(transform.position.x, transform.position.y) + (directionToCharge * chargeDistance), Color.green);
        }

        RaycastHit2D chargeCheck = Physics2D.Raycast(rgbd2D.position + raycastOffset, directionToCharge, chargeDistance);
        if(chargeCheck.collider != null)
        {
            if(isDebugging)
            { Debug.Log($"{this.name} has detected {chargeCheck.collider}."); }

            if(chargeCheck.collider.CompareTag("Player"))
            { Charge(); }
        }
        yield return s_checkDelay;
        StartCoroutine(CheckForPlayer());
    }

    private void Charge()
    {
        rgbd2D.AddForce(directionToCharge * chargeSpeed);
    }
}
