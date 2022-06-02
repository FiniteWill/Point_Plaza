using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Helper class for syncing a Transform to be the same as another given Transform.
/// given transform.
/// </summary>
public class SyncTransform : MonoBehaviour
{
    [SerializeField] private Transform originalTransform = null;
    [SerializeField] private Transform transformToSync = null;
    [SerializeField] private bool syncPosition = false;
    [SerializeField] private bool syncRotation = false;
    [SerializeField] private bool syncScale = false;

    private void Awake()
    {
        Assert.IsNotNull($"{name} does not have an original {originalTransform.GetType()} but requires one.");
        Assert.IsNotNull($"{name} does not have an {originalTransform.GetType()} to sync but requires one.");
    }
    // Called in LateUpdate in case the Transforms were affected by something in another script's Update.
    void LateUpdate()
    {
        if(originalTransform.hasChanged)
        {
            transformToSync.position = syncPosition ? originalTransform.position : transformToSync.position;
            transformToSync.localRotation = syncRotation ? originalTransform.localRotation : transformToSync.localRotation;
            transformToSync.localScale = syncScale ? originalTransform.localScale : transformToSync.localScale;
            originalTransform.hasChanged = false;
        }
    }
}
