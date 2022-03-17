using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum PlatformerAnimationState { Idle, RunLeft, RunRight, JumpLeft, JumpRight, JumpUp, TakeDamage, Die };
public class PlatformerPlayer_Animator : MonoBehaviour
{
    
    private PlatformerAnimationState currentState = PlatformerAnimationState.Idle;
    [SerializeField] private Dictionary<PlatformerAnimationState, Animation> animationDictionary = null;
    [SerializeField] private List<PlatformerAnimationState> animationStates = null;
    [SerializeField] private List<Animation> animations = null;

    private void Awake()
    {
        Assert.IsNotNull(animationDictionary, $"{this.name} does not have a {nameof(animationDictionary)} but requires one.");
        Assert.IsNotNull(animations, $"{this.name} does not have a {nameof(animations)} but requires one.");
        Assert.IsNotNull(animationStates, $"{this.name} does not have a {nameof(animationStates)} but requires one.");
        Assert.IsTrue(animationStates.Count == animations.Count, $"{this.name} does not have an equal number of {nameof(animations)} and {nameof(animationStates)}.");
        if(animationStates.Count == animations.Count)
        {
            foreach(Animation animation in animations)
            {
                foreach(PlatformerAnimationState state in animationStates)
                {
                    animationDictionary.Add(state, animation);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        animationDictionary.TryGetValue(currentState, out Animation temp);
        if (!temp.isPlaying)
        { temp.Play(); }
    }
}
