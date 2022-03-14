using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum PlatformerAnimationState { Idle, RunLeft, RunRight, JumpLeft, JumpRight, JumpUp, TakeDamage, Die };
public class PlatformerPlayer_Animator : MonoBehaviour
{
    
    private PlatformerAnimationState animationState = PlatformerAnimationState.Idle;
    [SerializeField] private Dictionary<PlatformerAnimationState, Animation> animationDictionary = null;

    private void Awake()
    {
        Assert.IsNotNull(animationDictionary, $"{this.name} does not have a {nameof(animationDictionary)} but requires one.");
    }

    // Update is called once per frame
    void Update()
    {
        animationDictionary.TryGetValue(animationState, out Animation temp);
        if (!temp.isPlaying)
        { temp.Play(); }
    }
}
