using UnityEngine;

/// <summary>
/// Helper class full of useful functions for manipulating Transforms.
/// </summary>
public static class TransformHelpers
{
    #region Position Functions
    public static void SetTransformPositions(Transform[] transforms, Vector3 position, bool useLocal)
    {
        foreach(Transform t in transforms)
        {
            if (useLocal) { t.localPosition = position; }
            else { t.position = position; }
        }
    }
    public static void ChangeTransformPositions(Transform[] transforms, Vector3 positionalChange, bool useLocal)
    {
        foreach(Transform t in transforms)
        {
            if (useLocal) { t.localPosition += positionalChange; }
            else { t.position += positionalChange; }
        }
    }
    #endregion Positions Functions
    #region Rotational Functions
    public static void SetTransformRotations(Transform[] transforms, Vector3 rotation, bool useLocal)
    {

        foreach(Transform t in transforms)
        {
            if (useLocal) { t.localEulerAngles = rotation; }
            else { t.eulerAngles = rotation; }
        }
    }
    public static void ChangeTransformRotations(Transform[] transforms, Vector3 rotation, bool useLocal)
    {
        foreach (Transform t in transforms)
        {
            if (useLocal) { t.localEulerAngles += rotation; }
            else { t.eulerAngles += rotation; }
        }
    }
    public static void ChangeTransformRotationAsChild(Transform child, Transform parent, Vector3 rotation, bool useLocal)
    {

    }
    #endregion Rotational Functions
    #region Scale Functions
    public static void SetTransformScales(Transform[] transforms, Vector3 scale)
    {
        foreach (Transform t in transforms)
        {
            t.localScale = scale;
        }
    }
    public static void ChangeTransformScales(Transform[] transforms, Vector3 scale)
    {
        foreach(Transform t in transforms)
        {
            t.localScale += scale;
        }
    }
    #endregion Scale Functions
}
