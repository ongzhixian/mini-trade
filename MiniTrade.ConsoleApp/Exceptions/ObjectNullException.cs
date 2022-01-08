using System.Runtime.Serialization;

namespace MiniTrade.ConsoleApp.Exceptions;

[Serializable]
public class ObjectNullException<T> : Exception where T : class
{
    public string? TypeName { get; set; } = null;
    public string ParamName { get; set; } = string.Empty;

    public ObjectNullException()
    {
    }

    public ObjectNullException(string paramName) 
    {
        ParamName = paramName;
        TypeName = typeof(T).FullName;
    }

    protected ObjectNullException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
