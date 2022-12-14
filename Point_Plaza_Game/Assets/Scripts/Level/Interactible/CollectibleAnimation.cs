using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Used to trigger Animation and wait before destroying collectible.
/// </summary>
public class CollectibleAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animName;
    public Animator Animator => animator;
    [SerializeField] private Collider2D triggerCollider;
    // Start is called before the first frame update

    private bool PlayAnimation()
    {
        animator.Play(animName);
        while (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            // wait for animation to end
        }
        return true;
    }
}
