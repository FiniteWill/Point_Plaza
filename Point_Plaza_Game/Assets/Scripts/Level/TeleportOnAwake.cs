using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnAwake : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform spawn;
    private float delay = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Teleport());
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(delay);
        player.position = spawn.position;
    }
}
