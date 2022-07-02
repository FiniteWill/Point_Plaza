using UnityEngine;

/// <summary>
/// Helper class containing functions for toggling different properties on a GameObject.
/// Useful for things like calling functions like SetActive() from UI buttons without
/// needing a parameter.
/// </summary>
public class TogglerHelper : MonoBehaviour
{
    public void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void ToggleActive(GameObject g)
    {
        g.SetActive(!g.activeSelf);
    }
    public void ToggleComponents(System.Type type)
    {
        var stuff = type.GetType();
        var temp_components = GetComponents<System.Type>();
    }
    public void ToggleComponents<T>() where T : Component
    {
        var temp_components = GetComponents<T>();
        if (typeof(T).IsSubclassOf(typeof(MonoBehaviour)))
        {
            foreach (var x in temp_components)
            {
                (x as MonoBehaviour).enabled = !(x as MonoBehaviour).enabled;
            }
        }
    }
    public void ToggleComponents<T>(bool toggle) where T : Component
    {
        var temp_components = GetComponents<T>();
        if (typeof(T).IsSubclassOf(typeof(MonoBehaviour)))
        {
            foreach (var x in temp_components)
            {
                (x as MonoBehaviour).enabled = toggle;
            } 
        }
    }
    /// <summary>
    /// Calls functions depending on toggle state.
    /// </summary>
    /// <typeparam name="T">Return type for <paramref name="onFunc"/>.</typeparam>
    /// <typeparam name="G">Return type for <paramref name="offFunc"/>.</typeparam>
    /// <param name="onFunc">Function called when <paramref name="toggle"/> is true.</param>
    /// <param name="offFunc">Function called when <paramref name="toggle"/> is false.</param>
    /// <param name="toggle">Toggle state determining what function gets called.</param>
    public void ToggleFunction<T,G>(IFunction<T> onFunc, IFunction<G> offFunc, bool toggle)
    {
        if (toggle) { onFunc.Function(); }
        else { offFunc.Function(); }
    }
    /// <summary>
    /// Calls functions with parameters depending on toggle state.
    /// </summary>
    /// <typeparam name="T">Return type for <paramref name="onFunc"/>.</typeparam>
    /// <typeparam name="G">Parm type for <paramref name="onParam"/>.</typeparam>
    /// <typeparam name="C">Return type for <paramref name="offFunc"/>.</typeparam>
    /// <typeparam name="H">Parm type for <paramref name="offParm"/>.</typeparam>
    /// <param name="onFunc">Function called when <paramref name="toggle"/> is true.</param>
    /// <param name="onParam">Parameter for <paramref name="onFunc"/>.</param>
    /// <param name="offFunc">Function called when <paramref name="toggle"/> is false.</param>
    /// <param name="offParm">Parameter for <paramref name="offFunc"/>.</param>
    /// <param name="toggle">Toggle state determining what function gets called.</param>
    public void ToggleFunction<T,G, C,H>(IFunctionWithParam<T,G> onFunc, G onParam, 
        IFunctionWithParam<C,H> offFunc, H offParm, bool toggle)
    {
        if (toggle) { onFunc.Function(onParam); }
        else { offFunc.Function(offParm); }
    }

}
