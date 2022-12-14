using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DetectionZone : MonoBehaviour
{
    private Collider2D detectionCollider;
    [SerializeField] private string playerTag;
    private SpriteRenderer spriteRenderer = null;
    private bool detected = false;
    private PlatformerPlayer_Movement player = null;

    private void Awake()
    {
        spriteRenderer.GetComponentInChildren<SpriteRenderer>();
        detectionCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(detected)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag))
        {
            player = collision.GetComponentInChildren<PlatformerPlayer_Movement>();
            if (player != null)
            {
                if (collision.transform.position.x < transform.position.x)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                Debug.LogError($"Detection zone on {name} found an object with Tag: {playerTag} but no attached {nameof(PlatformerPlayer_Movement)}");
            }
        }
    }
}
