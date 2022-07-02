using System.Collections;
/// <summary>
/// Interface for something that needs a function.
/// Used for calling a generic function from objects without
/// needing to know the context.
/// </summary>
/// <typeparam name="T">Return type of the function.</typeparam>
public interface IFunction<T>
{
    public T Function();
}
/// <summary>
/// Interface for something that needs a function that takes a parameter.
/// Used for calling a generic function with a parameter from objects
/// without needing to know the context. Parameter G can be set to a
/// generic container for sending multiple parameters.
/// </summary>
/// <typeparam name="T">Return type of the function.</typeparam>
/// <typeparam name="G">Parameter type of the function.</typeparam>
public interface IFunctionWithParam<T, G>
{
    public T Function(G param);
}
