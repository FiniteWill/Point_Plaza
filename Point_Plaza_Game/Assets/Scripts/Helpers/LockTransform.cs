using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Helper class for freezing rotation, position, and scale for a Transform object.
/// </summary>
public class LockTransform : MonoBehaviour
{
    [SerializeField] private Transform objTransform = null;
    [SerializeField] private bool posIsLocked = false;
    [SerializeField] private bool rotIsLocked = false;
    [SerializeField] private bool scaleIsLocked = false;

    private Vector3 startPos = Vector3.zero;
    private Vector3 startRot = Vector3.zero;
    private Vector3 startScale = Vector3.zero;

    // Setting the default position of the Transform to lock the values.
    private void Awake()
    {         
        Assert.IsNotNull($"{name} does not have an {objTransform.GetType()} but requires one.");

        startPos = objTransform.position;
        startRot = objTransform.localEulerAngles;
        startScale = objTransform.localScale;
    }
    // Called in LateUpdate in case any changes are made to the objects in Update.
    private void LateUpdate()
    {
        if(objTransform.hasChanged)
        {
            objTransform.position = posIsLocked ? startPos : objTransform.position;
            objTransform.localEulerAngles = rotIsLocked ? startRot : objTransform.localEulerAngles;
            objTransform.localScale = scaleIsLocked ? startScale : objTransform.localScale;
            objTransform.hasChanged = false;
        }
    }
}
