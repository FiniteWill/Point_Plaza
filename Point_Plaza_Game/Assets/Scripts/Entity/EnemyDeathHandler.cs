using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyDeathHandler : MonoBehaviour, IDeathHandler
{
    [SerializeField] private bool playAnimationOnDeath = true;
    private Animation deathAnimation = null;

    private void Awake()
    {
        Debug.Assert(deathAnimation, $"{name} could not find a {deathAnimation.GetType()} but requires one.");
    }

    public void HandleDeath()
    {
        // Play animation, and destroy this game object after the animation finishes playing.
        if(playAnimationOnDeath && deathAnimation != null)
        { deathAnimation.Play(); }

        while(deathAnimation.isPlaying)
        {
            // Wait
        }

        GameObject t_root = transform.root.gameObject;
        Destroy(t_root);

    }
}
