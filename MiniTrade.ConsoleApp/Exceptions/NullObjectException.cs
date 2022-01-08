namespace MiniTrade.ConsoleApp.Exceptions;

public class NullResultException<T> : Exception where T : class
{
    public T Result { get; private init; }

    public string TypeName { get; private init; }

    public NullResultException(T result)
    {
        Result = result;
        TypeName = typeof(T).FullName;
    }
}
