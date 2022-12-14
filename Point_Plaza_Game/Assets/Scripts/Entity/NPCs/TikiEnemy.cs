using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageZone))]
public class TikiEnemy : MonoBehaviour
{
    [SerializeField] private AudioSource firingSFX = null;
    [SerializeField] private bool constDelay = false;
    [SerializeField] private float attackDelay = 3f;
    [SerializeField] [Min(0f)] private float minDelay = 3f;
    [SerializeField] private float maxDelay = 5f;
    private float curDelay = 0f;

    [SerializeField] [Range(1f, 4f)] private float attackDuration;
    private float curDuration = 2f;
    private DamageZone damageZone = null;

    // Start is called before the first frame update
    void Start()
    {
        damageZone = GetComponent<DamageZone>();
    }

    // Update is called once per frame
    void Update()
    {
        if(curDelay > 0)
        {
            curDelay -= Time.deltaTime;
        }
        else
        {
            AudioManagerSingleton.Instance.PlayAudio(firingSFX);
            //firingSFX.Play();
            damageZone.gameObject.SetActive(true);
            curDelay = Random.Range(minDelay, maxDelay);
        }

        if(curDuration > 0)
        {
            curDuration -= Time.deltaTime;
        }
        else
        {
            damageZone.gameObject.SetActive(false);
            curDuration = attackDuration;
        }
    }

}
