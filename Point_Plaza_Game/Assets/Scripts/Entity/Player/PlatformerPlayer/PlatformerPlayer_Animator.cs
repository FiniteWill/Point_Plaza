using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Enums;

public class PlatformerPlayer_Animator : MonoBehaviour
{
    [SerializeField] private Dictionary<PlatformerAnimationState, Animation> animationDictionary = null;
    [SerializeField] private List<PlatformerAnimationState> animationStates = null;
    [SerializeField] private List<Animation> animations = null;

    private IEnumerator CR_PlayAnimation = null;
    private WaitForSeconds playAnimationDelay = new WaitForSeconds(0.1f);
    private PlatformerAnimationState currentState = PlatformerAnimationState.Idle;
    public PlatformerAnimationState GetAnimationState() { return currentState; }
    public void SetAnimationState(PlatformerAnimationState state) { currentState = state; }

    private void Awake()
    {
        Assert.IsNotNull(animationDictionary, $"{this.name} does not have a {nameof(animationDictionary)} but requires one.");
        Assert.IsNotNull(animations, $"{this.name} does not have a {nameof(animations)} but requires one.");
        Assert.IsNotNull(animationStates, $"{this.name} does not have a {nameof(animationStates)} but requires one.");
        Assert.IsTrue(animationStates.Count == animations.Count, $"{this.name} does not have an equal number of {nameof(animations)} and {nameof(animationStates)}.");
        if(animationStates.Count == animations.Count)
        {
            for(int i=0; i<animations.Count; ++i)
            {
                animationDictionary.Add(animationStates[i], animations[i]);
            }
        }
        CR_PlayAnimation = PlayAnimationCR();
    }

    private IEnumerator PlayAnimationCR()
    {
        animationDictionary.TryGetValue(currentState, out Animation temp);
        if (!temp.isPlaying)
        { temp.Play(); }
        yield return playAnimationDelay;
        StartCoroutine(CR_PlayAnimation);
    }

}
