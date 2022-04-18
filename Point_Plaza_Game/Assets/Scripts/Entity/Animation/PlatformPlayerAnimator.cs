using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class PlatformPlayerAnimator : MonoBehaviour, IEntityAnimator
{
    private Dictionary<Animation, int> animationList = null;
    private void Awake()
    {
        Assert.IsNotNull(animationList);
    }

    public void HandleDeath()
    {
        throw new System.NotImplementedException();
    }

    public void HandleSpawn()
    {
        throw new System.NotImplementedException();
    }
 
}
