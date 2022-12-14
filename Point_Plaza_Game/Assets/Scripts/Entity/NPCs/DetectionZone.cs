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

    private void Awake()
    {
        spriteRenderer.GetComponentInChildren<SpriteRenderer>();
        detectionCollider.isTrigger = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag))
        {
            if(collision.transform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;   
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
