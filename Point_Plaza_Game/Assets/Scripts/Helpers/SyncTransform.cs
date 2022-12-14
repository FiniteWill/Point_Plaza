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

    [SerializeField] private bool posX = false;
    [SerializeField] private bool posY = false;
    [SerializeField] private bool posZ = false;

    [SerializeField] private bool rotX = false;
    [SerializeField] private bool rotY = false;
    [SerializeField] private bool rotZ = false;

    [SerializeField] private bool scaleX = false;
    [SerializeField] private bool scaleY = false;
    [SerializeField] private bool scaleZ = false;

    private void Awake()
    {
        Assert.IsNotNull($"{name} does not have an original {originalTransform.GetType()} but requires one.");
        Assert.IsNotNull($"{name} does not have an {originalTransform.GetType()} to sync but requires one.");
    }
    // Called in LateUpdate in case the Transforms were affected by something in another script's Update.
    void LateUpdate()
    {
        if (originalTransform.hasChanged)
        {
            Vector3 newPos;
            newPos.x = posX ? originalTransform.position.x : transformToSync.position.x;
            newPos.y = posY ? originalTransform.position.y : transformToSync.position.y;
            newPos.z = posZ ? originalTransform.position.z : transformToSync.position.z;
            transformToSync.position = newPos;

            Vector3 newRot;
            newRot.x = rotX ? originalTransform.localRotation.x : transformToSync.localRotation.x;
            newRot.y = rotY ? originalTransform.localRotation.y : transformToSync.localRotation.y;
            newRot.z = rotZ ? originalTransform.localRotation.z : transformToSync.localRotation.z;
            transformToSync.localEulerAngles = newRot;

            Vector3 newScale;
            newScale.x = scaleX ? originalTransform.localScale.x : transformToSync.localScale.x;
            newScale.y = scaleY ? originalTransform.localScale.y : transformToSync.localScale.y;
            newScale.z = scaleZ ? originalTransform.localScale.z : transformToSync.localScale.z;
            transformToSync.localScale = newScale;

            originalTransform.hasChanged = false;
        }
    }
}